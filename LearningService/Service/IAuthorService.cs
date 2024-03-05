using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IAuthorService
{
    ValueTask<Author> CreateAuthorAsync(AuthorDto article);
    ValueTask<Author> UpdateAuthorAsync(AuthorDto article);
    ValueTask<Author> DeleteAuthorAsync(int articleId);
    ValueTask<AuthorDto> GetAuthorByIdAsync(int id);
    ValueTask<IList<AuthorDto>> GetAllAuthorAsync();
}
