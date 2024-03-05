using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IQuizService
{
    ValueTask<QuizDto> CreateQuizAsync(QuizDto quizDto);
    ValueTask<QuizDto> UpdateQuizAsync(QuizDto quizDto);
    ValueTask<QuizDto> DeleteQuizAsync(int id);
    ValueTask<QuizDto> GetQuizByIdAsync(int id);
    ValueTask<IList<QuizDto>> GetAllQuizAsync();
    ValueTask<QuestionDto> CreateQuestionAsync(QuestionDto questionDto);
    ValueTask<QuestionDto> UpdateQuestionAsync(QuestionDto quiz);
    ValueTask<QuestionDto> DeleteQuestionAsync(long id);
    ValueTask<(QuizDto,List<QuestionDto>)> GetQuizIncludeQuestionsByIdAsync(int id);
}