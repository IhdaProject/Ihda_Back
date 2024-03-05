using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionRepository _questionRepository;

    public QuizService(IQuizRepository quizRepository,
        IQuestionRepository questionRepository)
    {
        _quizRepository = quizRepository;
        _questionRepository = questionRepository;
    }

    public async ValueTask<QuizDto> CreateQuizAsync(QuizDto quizDto)
    {
        var quiz = new Quiz()
        {
            CourseId = quizDto.courseId,
            Description = quizDto.description,
            Duration = new TimeSpan(minutes:quizDto.durationMinutes,hours:0,seconds:0),
            Title = quizDto.title,
            TotalScore = quizDto.totalScore,
            PassingScore = quizDto.passingScore
        };

        return this.QuizToDto(await _quizRepository.AddAsync(quiz));
    }

    public async ValueTask<QuizDto> DeleteQuizAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not found");

        return this.QuizToDto(await _quizRepository.RemoveAsync(quiz));
    }

    public async ValueTask<IList<QuizDto>> GetAllQuizAsync()
    {
        return await _quizRepository
            .GetAllAsQueryable()
            .Select(q => this.QuizToDto(q))
            .ToListAsync();
    }

    public async ValueTask<QuestionDto> CreateQuestionAsync(QuestionDto questionDto)
    {
        var question = new SimpleQuestion()
        {
            QuizId = questionDto.quizId,
            QuestionContent = questionDto.content,
            OrderNumber = questionDto.orderNumber,
            DocLink = questionDto.docLink,
            ImageLink = questionDto.imageLink,
            TotalBall = questionDto.ball,
            Options = questionDto.options
        };

        var simpleQuestion = await _questionRepository.AddAsync(question);

        return new QuestionDto(
            simpleQuestion.Id,
            simpleQuestion.QuizId,
            simpleQuestion.OrderNumber,
            simpleQuestion.QuestionContent,
            simpleQuestion.ImageLink,
            simpleQuestion.DocLink,
            simpleQuestion.TotalBall,
            question.Options);
    }

    public async ValueTask<QuestionDto> UpdateQuestionAsync(QuestionDto questionDto)
    {
        var question = await _questionRepository
            .OfType<SimpleQuestion>()
            .OrderBy(q => q.OrderNumber)
            .FirstOrDefaultAsync(q => 
                q.Id == (int)questionDto.id!)
            ?? throw new NotFoundException("Not found");

        question.QuizId = questionDto.quizId != 0 ? questionDto.quizId : question.QuizId;
        question.QuestionContent = questionDto.content ?? question.QuestionContent;
        question.OrderNumber = questionDto.orderNumber != 0 ? questionDto.orderNumber : question.OrderNumber;
        question.DocLink = questionDto.docLink ?? question.DocLink;
        question.ImageLink = questionDto.imageLink ?? question.ImageLink;
        question.TotalBall = questionDto.ball ?? question.TotalBall;
        question.Options = questionDto.options.Count != 0 ? questionDto.options : question.Options;

        await _questionRepository.UpdateAsync(question);

        return new QuestionDto(
            question.Id,
            question.QuizId,
            question.OrderNumber,
            question.QuestionContent,
            question.ImageLink,
            question.DocLink,
            question.TotalBall,
            null);
    }

    public async ValueTask<QuestionDto> DeleteQuestionAsync(long id)
    {
        var question = await _questionRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException("Not found");

        var simpleQuestion = await _questionRepository.RemoveAsync(question);
        
        return new QuestionDto(
            question.Id,
            question.QuizId,
            question.OrderNumber,
            question.QuestionContent,
            question.ImageLink,
            question.DocLink,
            question.TotalBall,
            null);
    }

    public async ValueTask<(QuizDto,List<QuestionDto>)> GetQuizIncludeQuestionsByIdAsync(int id)
    {
        var quiz = await _quizRepository
                       .GetAllAsQueryable()
                       .FirstOrDefaultAsync(q => q.CourseId == id);

        if (quiz is null)
            return (null,null);

        var questions = await _questionRepository.OfType<SimpleQuestion>()
            .Where(q => q.QuizId == quiz.Id)
            .OrderBy(q => q.OrderNumber)
            .Select(question => new QuestionDto(
                question.Id,
                question.QuizId,
                question.OrderNumber,
                question.QuestionContent,
                question.ImageLink,
                question.DocLink,
                question.TotalBall,
                question.Options))
            .ToListAsync();

        return (this.QuizToDto(quiz), questions);
    }

    public async ValueTask<QuizDto> GetQuizByIdAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not found");

        return this.QuizToDto(quiz);
    }

    public async ValueTask<QuizDto> UpdateQuizAsync(QuizDto quizDto)
    {
        var resultQuiz = await _quizRepository.GetByIdAsync((long)quizDto.id)
            ?? throw new NotFoundException("Not found");

        resultQuiz.Title = quizDto.title ?? resultQuiz.Title;
        resultQuiz.Description = quizDto.description ?? resultQuiz.Description;
        resultQuiz.Duration = (quizDto.durationMinutes != 0) ? new TimeSpan(0,quizDto.durationMinutes,0) : resultQuiz.Duration;
        resultQuiz.TotalScore = (quizDto.totalScore != 0) ? quizDto.totalScore : resultQuiz.TotalScore;
        resultQuiz.PassingScore = (quizDto.passingScore is not 0) ? quizDto.passingScore : resultQuiz.PassingScore;

        return this.QuizToDto(await _quizRepository.UpdateAsync(resultQuiz));
    }

    private QuizDto QuizToDto(Quiz quiz)
    {
        return new QuizDto(
            quiz.Id,
            quiz.CourseId,
            quiz.Title,
            quiz.Description,
            quiz.TotalScore,
            (int)quiz.PassingScore,
            quiz.Duration.Minutes);
    }
}
