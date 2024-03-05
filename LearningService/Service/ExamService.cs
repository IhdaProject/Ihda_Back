using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Enum;
using Entity.Exeptions;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IQuestionInExamRepository _questionInExamRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;

    public ExamService(
        IExamRepository examRepository,
        IQuestionInExamRepository questionInExamRepository,
        IQuestionRepository questionRepository,
        IQuizRepository quizRepository)
    {
        _examRepository = examRepository;
        _questionInExamRepository = questionInExamRepository;
        _questionRepository = questionRepository;
        _quizRepository = quizRepository;
    }

    public async ValueTask<ExamDto> CreateExamAsync(long userId, long quizId)
    {
        var exams = await _examRepository.GetAllAsQueryable()
            .Where(e => e.QuizId == quizId)
            .Where(e => e.UserId == userId)
            .ToListAsync();

        var exam = exams
            .Where(e => DateTime.UtcNow - e.CreatedAt <= e.Quiz.Duration)
            .FirstOrDefault(e => e.Status == ExamStatus.Progress);
        
        var quiz = await _quizRepository.GetByIdAsync(quizId)
                   ?? throw new NotFoundException("Not found quiz");
        
        var heart = 1;
        if (exams.Count > 0)
        {
            heart = exams.Max(e => e.UsedHeart) + 1;
            if (quiz.Heart < heart)
                throw new AlreadyExistsException("There are no attempts(hearts) left");
        }

        if (exam is null && exams.All(e => e.CreatedAt.Date != DateTime.UtcNow.Date))
        {
            var questions = await _questionRepository.GetAllAsQueryable()
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
            
            var rnd = new Random();

            questions = questions.Select(q => (q, rnd.Next()))
                .OrderBy(tuple => tuple.Item2)
                .Select(tuple => tuple.Item1)
                .ToList(); // Shuffle array questions

            var questionInExams = new List<QuestionInExam>();

            foreach (var question in questions)
            {
                QuestionInExam questionInExam = null;
                switch (question.QuestionType)
                {
                    case QuestionTypes.Simple:
                    {
                        var simpleQuestion = question as SimpleQuestion;
                        questionInExam = new SimpleQuestionInExam()
                        {
                            QuestionType = simpleQuestion.QuestionType,
                            QuestionId = simpleQuestion.Id,
                            Options = simpleQuestion.Options.Select(x => (x, rnd.Next()))
                                .OrderBy(tuple => tuple.Item2)
                                .Select(tuple => tuple.Item1)
                                .ToList() // Shuffle array options
                        };
                        break;
                    }
                    case QuestionTypes.Written:
                    {
                        var writtenQuestion = question as WrittenQuestion;
                        questionInExam = new WrittenQuestionInExam()
                        {
                            QuestionType = writtenQuestion.QuestionType,
                            QuestionId = writtenQuestion.Id
                        };
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                questionInExams.Add(questionInExam);
            }
            exam = new Exam()
            {
                QuizId = quizId,
                UserId = userId,
                Questions = questionInExams,
                UsedHeart = heart
            };

            exam = await _examRepository.AddAsync(exam);
            exam.Quiz = quiz;
        }
        else if(exams.Any(e => e.CreatedAt.Date == DateTime.UtcNow.Date) && exam is null)
            throw new AlreadyExistsException("Done for today");
        
        return new ExamDto(exam.Id,
            exam.QuizId,
            exam.Quiz.Title,
            (long)exam.CreatedAt.Subtract(new DateTime(1970,1,1)).TotalSeconds,
            (long)exam.CreatedAt.Subtract(new DateTime(1970,1,1)).TotalSeconds + (long)exam.Quiz.Duration.TotalSeconds,
            exam.Status,
            exam.UsedHeart,
            exam.Questions
                .Select(q =>
                {
                    switch (q.QuestionType)
                    {
                        case QuestionTypes.Simple:
                        {
                            var simpleQuestionInExam = q as SimpleQuestionInExam;
                            return new QuestionInExamDto(
                                simpleQuestionInExam.Id,
                                simpleQuestionInExam.ExamId,
                                simpleQuestionInExam.QuestionType,
                                simpleQuestionInExam.Question.OrderNumber,
                                simpleQuestionInExam.Question.QuestionContent,
                                simpleQuestionInExam.Question.ImageLink,
                                simpleQuestionInExam.Question.DocLink,
                                null,
                                null,
                                null,
                                simpleQuestionInExam.Selected,
                                simpleQuestionInExam.Options.Select(o => new SimpleQuestionOption()
                                {
                                    AnswerContent = o.AnswerContent,
                                    AnswerId = o.AnswerId
                                }).ToList());
                        }
                        case QuestionTypes.Written:
                        {
                            var writtenQuestionInExam = q as WrittenQuestionInExam;
                            return new QuestionInExamDto(
                                writtenQuestionInExam.Id,
                                writtenQuestionInExam.ExamId,
                                writtenQuestionInExam.QuestionType,
                                writtenQuestionInExam.Question.OrderNumber,
                                writtenQuestionInExam.Question.QuestionContent,
                                writtenQuestionInExam.Question.ImageLink,
                                writtenQuestionInExam.Question.DocLink,
                                writtenQuestionInExam.WrittenAnswer,
                                writtenQuestionInExam.Checked,
                                writtenQuestionInExam.AccumulatedBall,
                                null,
                                null);
                        }
                        default:
                            return null;
                    }
                })
                .ToList());
    }

    public async ValueTask<ExamDto> CompletionExamAsync(ExamDto examDto)
    {
        var exam = await _examRepository.GetByIdAsync(examDto.id);

        if (exam.Status == ExamStatus.Finished)
        {
            throw new AlreadyExistsException("Finished before that");
        }
        
        if (exam.CreatedAt.Add(exam.Quiz.Duration).AddSeconds(20) < DateTime.UtcNow)
        {
            exam.ClosedAt = exam.CreatedAt.Add(exam.Quiz.Duration);
            exam.Status = ExamStatus.Finished;
            await _examRepository.UpdateAsync(exam);
            throw new AlreadyExistsException("Time is up");
        }
        
        exam.ClosedAt = DateTime.UtcNow;
        exam.Status = ExamStatus.Finished;
        var dicQuestion = examDto.questions.ToDictionary(q => q.id);
        exam.Questions
            .Where(q => dicQuestion.ContainsKey(q.Id))
            .Select(async q =>
            {
                switch (q.QuestionType)
                {
                    case QuestionTypes.Simple: 
                    {
                        var sq = q as SimpleQuestionInExam;
                        sq.Selected ??= dicQuestion[sq.Id].selected;
                        q = await _questionInExamRepository.UpdateAsync(sq);
                        return q;
                    } 
                    case QuestionTypes.Written: 
                    {
                        var wq = q as WrittenQuestionInExam;
                        wq.WrittenAnswer ??= dicQuestion[wq.Id].writtenAnswer;
                        q = await _questionInExamRepository.UpdateAsync(wq);
                        return q; 
                    } 
                    default: 
                        return null;
                }
            }).ToList();

        exam = await _examRepository.UpdateAsync(exam);
        
        return new ExamDto(exam.Id,
            exam.QuizId,
            exam.Quiz.Title,
            (long)exam.CreatedAt.Subtract(new DateTime(1970,1,1)).TotalSeconds,
            (long)exam.ClosedAt?.Subtract(new DateTime(1970,1,1)).TotalSeconds,
            exam.Status,
            exam.UsedHeart,
            exam.Questions
                .Select(q =>
                {
                    switch (q.QuestionType)
                    {
                        case QuestionTypes.Simple:
                        {
                            var simpleQuestionInExam = q as SimpleQuestionInExam;
                            return new QuestionInExamDto(
                                simpleQuestionInExam.Id,
                                simpleQuestionInExam.ExamId,
                                simpleQuestionInExam.QuestionType,
                                simpleQuestionInExam.Question.OrderNumber,
                                simpleQuestionInExam.Question.QuestionContent,
                                simpleQuestionInExam.Question.ImageLink,
                                simpleQuestionInExam.Question.DocLink,
                                null,
                                null,
                                null,
                                simpleQuestionInExam.Selected,
                                simpleQuestionInExam.Options.Select(o => new SimpleQuestionOption()
                                {
                                    AnswerContent = o.AnswerContent,
                                    AnswerId = o.AnswerId,
                                    Ball = o.Ball
                                }).ToList());
                        }
                        case QuestionTypes.Written:
                        {
                            var writtenQuestionInExam = q as WrittenQuestionInExam;
                            return new QuestionInExamDto(
                                writtenQuestionInExam.Id,
                                writtenQuestionInExam.ExamId,
                                writtenQuestionInExam.QuestionType,
                                writtenQuestionInExam.Question.OrderNumber,
                                writtenQuestionInExam.Question.QuestionContent,
                                writtenQuestionInExam.Question.ImageLink,
                                writtenQuestionInExam.Question.DocLink,
                                writtenQuestionInExam.WrittenAnswer,
                                writtenQuestionInExam.Checked,
                                writtenQuestionInExam.AccumulatedBall,
                                null,
                                null);
                        }
                        default:
                            return null;
                    }
                })
                .ToList());
    }

    public async ValueTask<ExamResultDto> InformationExamAsync(long examId)
    {
        var exam = await _examRepository.GetByIdAsync(examId);
        
        return new ExamResultDto(
            exam.Id,
            new Quiz(){Description = exam.Quiz.Description,Title = exam.Quiz.Title},
            exam.CreatedAt,
            (DateTime)exam.ClosedAt,
            exam.Status,
            (DateTime)exam.ClosedAt - exam.CreatedAt,
            exam.UsedHeart,
            exam.Questions
            .Sum(question =>
            {
                switch (question.QuestionType)
                {
                    case QuestionTypes.Simple:
                    {
                        var simpleQuestion = question as SimpleQuestionInExam;
                        return simpleQuestion.Options.Where(o =>
                                o.AnswerId == simpleQuestion.Selected)
                            .Sum(o => o.Ball);
                    }
                    case QuestionTypes.Written:
                    {
                        var writtenQuestion = question as WrittenQuestionInExam;
                        return writtenQuestion.AccumulatedBall;
                    }
                    default:
                        return 0;
                }
            }),
            exam.Questions
                .Select(q =>
                {
                    switch (q.QuestionType)
                    {
                        case QuestionTypes.Simple:
                        {
                            var simpleQuestionInExam = q as SimpleQuestionInExam;
                            return new QuestionInExamDto(
                                simpleQuestionInExam.Id,
                                simpleQuestionInExam.ExamId,
                                simpleQuestionInExam.QuestionType,
                                simpleQuestionInExam.Question.OrderNumber,
                                simpleQuestionInExam.Question.QuestionContent,
                                simpleQuestionInExam.Question.ImageLink,
                                simpleQuestionInExam.Question.DocLink,
                                null,
                                null,
                                null,
                                simpleQuestionInExam.Selected,
                                simpleQuestionInExam.Options.Select(o => new SimpleQuestionOption()
                                {
                                    AnswerContent = o.AnswerContent,
                                    AnswerId = o.AnswerId,
                                    Ball = 0
                                }).ToList());
                        }
                        case QuestionTypes.Written:
                        {
                            var writtenQuestionInExam = q as WrittenQuestionInExam;
                            return new QuestionInExamDto(
                                writtenQuestionInExam.Id,
                                writtenQuestionInExam.ExamId,
                                writtenQuestionInExam.QuestionType,
                                writtenQuestionInExam.Question.OrderNumber,
                                writtenQuestionInExam.Question.QuestionContent,
                                writtenQuestionInExam.Question.ImageLink,
                                writtenQuestionInExam.Question.DocLink,
                                writtenQuestionInExam.WrittenAnswer,
                                writtenQuestionInExam.Checked,
                                writtenQuestionInExam.AccumulatedBall,
                                null,
                                null);
                        }
                        default:
                            return null;
                    }
                })
                .ToList());
    }

    public async ValueTask<List<ExamForListDto>> GetExamsByUserAsync(long userId)
    {
        var exams =  await _examRepository.GetAllAsQueryable()
            .Where(e => e.UserId == userId)
            .Select(e => new {
                e.Id,
                e.CreatedAt,
                e.ClosedAt,
                e.Status,
                e.UsedHeart,
                e.Quiz.TotalScore,
                e.Questions
            })
            .ToListAsync();

        return exams.Select(e => new ExamForListDto(
                e.Id,
                e.CreatedAt,
                e.ClosedAt,
                e.Status,
                e.UsedHeart,
                e.TotalScore,
                e.Questions
                    .Sum(question =>
                    {
                        switch (question.QuestionType)
                        {
                            case QuestionTypes.Simple:
                            {
                                var simpleQuestion = question as SimpleQuestionInExam;
                                return simpleQuestion.Options.Where(o =>
                                        o.AnswerId == simpleQuestion.Selected)
                                    .Sum(o => o.Ball);
                            }
                            case QuestionTypes.Written:
                            {
                                var writtenQuestion = question as WrittenQuestionInExam;
                                return writtenQuestion.AccumulatedBall;
                            }
                            default:
                                return 0;
                        }
                    })))
            .ToList();
    }

    public async ValueTask<QuestionInExamDto> ReplyQuestionAsync(QuestionInExamDto questionInExamDto)
    {
        var questionInExam = await _questionInExamRepository.GetByIdAsync(questionInExamDto.id);
        
        switch (questionInExam.QuestionType)
        {
            case QuestionTypes.Simple:
            {
                var simpleQuestion = questionInExam as SimpleQuestionInExam;
                simpleQuestion.Selected = questionInExamDto.selected;
                questionInExam = await _questionInExamRepository.UpdateAsync(simpleQuestion);
                return new QuestionInExamDto(
                    questionInExam.Id,
                    questionInExam.ExamId,
                    questionInExam.QuestionType,
                    questionInExam.Question.OrderNumber,
                    questionInExam.Question.QuestionContent,
                    questionInExam.Question.ImageLink,
                    questionInExam.Question.DocLink,
                    null,
                    null,
                    null,
                    simpleQuestion.Selected,
                    simpleQuestion.Options);
            }
            case QuestionTypes.Written:
            {
                var writtenQuestion = questionInExam as WrittenQuestionInExam;
                writtenQuestion.WrittenAnswer = questionInExamDto.writtenAnswer;
                questionInExam = await _questionInExamRepository.UpdateAsync(writtenQuestion);
                return new QuestionInExamDto(
                    questionInExam.Id,
                    questionInExam.ExamId,
                    questionInExam.QuestionType,
                    questionInExam.Question.OrderNumber,
                    questionInExam.Question.QuestionContent,
                    questionInExam.Question.ImageLink,
                    questionInExam.Question.DocLink,
                    writtenQuestion.WrittenAnswer,
                    writtenQuestion.Checked,
                    writtenQuestion.AccumulatedBall,
                    null,
                    null);
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task<QuizInfoDto> GetQuizByCourseIdAsync(long userId,long courseId)
    {
        var quiz = await _quizRepository.GetAllAsQueryable()
            .Where(q => q.CourseId == courseId)
            .Select(q => new QuizInfoDto(
                q.Id,
                q.Title,
                q.Description,
                q.TotalScore,
                q.PassingScore,
                q.Duration.Minutes,
                _questionRepository.Count(ques => ques.QuizId == q.Id),
                q.Heart - _examRepository.Where(e => e.QuizId == q.Id && e.UserId == userId).Max(e => e.UsedHeart)))
            .FirstOrDefaultAsync();

        return quiz;
    }
}