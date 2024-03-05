using Entity.DataTransferObjects.Learning;

namespace LearningService.Service;

public interface IExamService
{
    ValueTask<ExamDto> CreateExamAsync(long userId, long quizId);
    ValueTask<ExamDto> CompletionExamAsync(ExamDto examDto);
    ValueTask<ExamResultDto> InformationExamAsync(long examId);
    ValueTask<List<ExamForListDto>> GetExamsByUserAsync(long userId);
    ValueTask<QuestionInExamDto> ReplyQuestionAsync(QuestionInExamDto questionInExamDto);
    Task<QuizInfoDto> GetQuizByCourseIdAsync(long userId,long courseId);
}