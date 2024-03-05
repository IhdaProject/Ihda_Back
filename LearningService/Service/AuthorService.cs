using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Author> CreateAuthorAsync(AuthorDto article)
    {
        var result = new Author()
        {
            Name = article.name,
            Content = article.content,
            ImageLinc = article.imageLink ?? "",
            Categories = article.categorieIds?.Select(c => new AuthorToCategory(){CategoryId = c}).ToList()
        };
        
        return await _repository.AddAsync(result);
    }

    public async ValueTask<Author> DeleteAuthorAsync(int articleId)
    {
        var articleResult = await _repository.GetByIdAsync(articleId)
           ?? throw new NotFoundException("Logotype not found");

        return await _repository.RemoveAsync(articleResult);
    }

    public async ValueTask<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not Found");

        return new AuthorDto(article.Name,article.Content,article.ImageLinc,article.Id,
            article.Categories.Select(c => c.CategoryId).ToList(),
            article.Categories.Select(c=>c.Category).ToList(),
            article.Courses.Count,
            article.ShortVideos.Count,
            article.SeminarVideos.Count,
            article.Articles.Count);
    }

    public async ValueTask<IList<AuthorDto>> GetAllAuthorAsync()
    {
        var articles = await _repository
           .GetAllAsQueryable()
           .Select(author => new AuthorDto(author.Name,author.Content,author.ImageLinc,author.Id
               ,author.Categories.Select(c => c.CategoryId).ToList(),
               author.Categories.Select(c=>c.Category).ToList(),0,0,0,0))
           .ToListAsync();

        return articles;
    }

    public async ValueTask<Author> UpdateAuthorAsync(AuthorDto article)
    {
        var articleResult = await _repository.GetByIdAsync(article.id)
            ?? throw new NotFoundException("Not found");

        articleResult.Name = article.name ?? articleResult.Name;
        articleResult.Content = article.content ?? articleResult.Content;
        articleResult.ImageLinc = article.imageLink ?? "";
        
        articleResult.Categories.Clear();
        
        articleResult.Categories = article.categorieIds
                    .Select(id => new AuthorToCategory(){CategoryId = id})
                    .ToList();

        return await _repository.UpdateAsync(articleResult);
    }
}
