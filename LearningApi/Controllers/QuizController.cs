using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class QuizController : ApiControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateQuiz(QuizDto quizDto)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.CreateQuizAsync(quizDto));
    }
    
    [HttpPost]
    public async ValueTask<ResponseModel> CreateQuestion(QuestionDto questionDto)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.CreateQuestionAsync(questionDto));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllQuiz([FromQuery]int courseId)
    {
        var res = await _quizService.GetQuizIncludeQuestionsByIdAsync(courseId);
        return ResponseModel
            .ResultFromContent(new {quiz = res.Item1,questions = res.Item2});
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetQuizById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.GetQuizByIdAsync(id));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateQuiz(QuizDto quizDto)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.UpdateQuizAsync(quizDto));
    }
    
    [HttpPut]
    public async ValueTask<ResponseModel> UpdateQuestion(QuestionDto questionDto)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.UpdateQuestionAsync(questionDto));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteQuiz(int id)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.DeleteQuizAsync(id));
    }
    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteQuestion(long id)
    {
        return ResponseModel
            .ResultFromContent(await _quizService.DeleteQuestionAsync(id));
    }
}
