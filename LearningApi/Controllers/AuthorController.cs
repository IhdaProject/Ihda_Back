using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthorController : ApiControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateAuthor(AuthorDto author)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.CreateAuthorAsync(author));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllAuthor()
    {
        return ResponseModel
            .ResultFromContent(await _authorService.GetAllAuthorAsync());
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAuthorById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.GetAuthorByIdAsync(id));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateAuthor(AuthorDto author)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.UpdateAuthorAsync(author));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteAuthor(int id)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.DeleteAuthorAsync(id));
    }
}