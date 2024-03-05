using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;


public class HashtagService : IHashtagService
{
    private readonly IHashtagRepository _repository;
    private readonly IShortVideoRepository _shortVideoRepository;
    private readonly ISeminarVideoRepository _seminarVideoRepository;
    private readonly IArticleRepository _articleRepository;

    public HashtagService(IHashtagRepository repository,
        IArticleRepository articleRepository,
        IShortVideoRepository shortVideoRepository,
        ISeminarVideoRepository seminarVideoRepository)
    {
        _repository = repository;
        _articleRepository = articleRepository;
        _shortVideoRepository = shortVideoRepository;
        _seminarVideoRepository = seminarVideoRepository;
    }

    public async ValueTask<Hashtag> CreateHashtagAsync(HashtagDto article)
    {
        var result = new Hashtag()
        {
            Name = article.name
        };
        return await _repository.AddAsync(result);
    }

    public async ValueTask<Hashtag> DeleteHashtagAsync(int articleId)
    {
        var articleResult = await _repository.GetByIdAsync(articleId)
           ?? throw new NotFoundException("Logotype not found");

        return await _repository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<Hashtag>> GetAllHashtagAsync(MetaQueryModel metaQuery)
    {
        var articles = await _repository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .Skip(metaQuery.Skip)
           .Take(metaQuery.Take)
           .ToListAsync();

        return articles;
    }

    public async ValueTask<GetByHashtagIdDto> GetAllByHashtagId(int hashtagId, string category, MetaQueryModel metaQuery)
    {
        var hashtag = await _repository
            .GetByIdAsync(hashtagId);

        var result = new GetByHashtagIdDto()
        {
            id = hashtag.Id,
            name = hashtag.Name,
            shortVideo = new List<ShortVideo>(),
            seminarVideo = new List<SeminarVideo>(),
            article = new List<Article>()
        };
        if(category == "shortvideo")
        {
            var newShortVideo = _shortVideoRepository
                .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
                .Where(x => x.HashtagId.Any(y => y == hashtagId))
                .Skip(metaQuery.Skip)
                .Take(metaQuery.Take)
                .ToList();
           
            result.shortVideo = newShortVideo;
        }
        else
        {
            var newSeminarVideo = _seminarVideoRepository
                .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
                .Where(x => x.HashtagId.Any(y => y == hashtagId))
                .Skip(metaQuery.Skip)
                .Take(metaQuery.Take)
                .ToList();
            var newArticle = _articleRepository
               .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
               .Where(x => x.HashtagId.Any(y => y == hashtagId))
               .Skip(metaQuery.Skip)
               .Take(metaQuery.Take)
               .ToList();

            result.article = newArticle;
            result.seminarVideo = newSeminarVideo;
        }

        return result;
    }

    public async ValueTask<Hashtag> GetHashtagByIdAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);

        return article;
    }

    public async ValueTask<Hashtag> UpdateHashtagAsync(Hashtag article)
    {
        var articleResult = await _repository.GetByIdAsync(article.Id)
            ?? throw new NotFoundException("Logotype not found");

        articleResult.Name = article.Name ?? articleResult.Name;

        return await _repository.UpdateAsync(articleResult);
    }
}
