using Entity.DataTransferObjects.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ExamController : ApiControllerBase
{
    private readonly IExamService _examService;

    public ExamController(IExamService examService)
    {
        _examService = examService;
    }
    [HttpGet]
    public async Task<ResponseModel> GetQuizByCourseId([FromQuery] long courseId)
    {
        return ResponseModel.ResultFromContent(
            await _examService.GetQuizByCourseIdAsync(this.UserId,courseId));
    }

    [HttpPost]
    public async Task<ResponseModel> Create([FromBody] CreateExamDto examDto)
    {
        return ResponseModel.ResultFromContent(
            await _examService.CreateExamAsync(this.UserId, examDto.quizId));
    }
    [HttpPut]
    public async Task<ResponseModel> Completion([FromBody]ExamDto examDto)
    {
        return ResponseModel.ResultFromContent(
            await _examService.CompletionExamAsync(examDto));
    }

    [HttpGet]
    public async Task<ResponseModel> GetExamResultById([FromQuery] long examId)
    {
        return ResponseModel.ResultFromContent(
            await _examService.InformationExamAsync(examId));
    }
    [HttpGet]
    public async Task<ResponseModel> GetExamsByUser()
    {
        return ResponseModel.ResultFromContent(
            await _examService.GetExamsByUserAsync(this.UserId));
    }
    [HttpPost]
    public async Task<ResponseModel> ReplyQuestion([FromBody]QuestionInExamDto questionInExamDto)
    {
        return ResponseModel.ResultFromContent(
            await _examService.ReplyQuestionAsync(questionInExamDto));
    }
}