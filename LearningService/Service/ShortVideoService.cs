using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class ShortVideoService : IShortVideoService
{
    private readonly IShortVideoRepository _shortVideoRepository;
    private readonly IHashtagRepository _hashtagRepository;

    public ShortVideoService(IShortVideoRepository shortVideoRepository,
        IHashtagRepository hashtagRepository)
    {
        _shortVideoRepository = shortVideoRepository;
        _hashtagRepository = hashtagRepository;
    }

    public async ValueTask<ShortVideo> CreateShortVideoAsync(ShortVideoDto article)
    {
        var result = new ShortVideo()
        {
            Title = article.title,
            VideoLinc = article.videoLinc,
            AuthorId = article.authorId,
            CategoryId = article.categoryId,
            HashtagId = article.hashtagId,            
        };

        return await _shortVideoRepository.AddAsync(result);
    }

    public async ValueTask<ShortVideo> DeleteShortVideoAsync(int articleId)
    {
        var  articleResult = await _shortVideoRepository.GetByIdAsync(articleId)
           ?? throw new NotFoundException("Logotype not found");

        return await _shortVideoRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<ShortVideo>> GetAllShortVideoAsync(MetaQueryModel metaQuery)
    {
        var shortVideos = await _shortVideoRepository
          .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
          .Skip(metaQuery.Skip)
          .Take(metaQuery.Take)
          .Select(sh => new ShortVideo()
          {
              Id = sh.Id,
              VideoLinc = sh.VideoLinc,
              Title = sh.Title,
              AuthorId = sh.AuthorId,
              Author = new Author(){Id = sh.Author.Id,Name = sh.Author.Name,Content = sh.Author.Content,ImageLinc = sh.Author.ImageLinc},
              CategoryId = sh.CategoryId,
              Category = new Category(){Id = sh.Category.Id,Title = sh.Category.Title,Description = sh.Category.Description,ImageLinc = sh.Category.ImageLinc},
              HashtagId = sh.HashtagId
          })
          .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in shortVideos)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return shortVideos;
    }

    public async ValueTask<IList<ShortVideo>> GetShortVideoByAftorIdAsync(MetaQueryModel metaQuery, int aftorId)
    {
        var articles =  await _shortVideoRepository
          .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
          .Where(x => x.AuthorId == aftorId)
          .Skip(metaQuery.Skip)
          .Take(metaQuery.Take)
          .Select(sh => new ShortVideo()
          {
              Id = sh.Id,
              VideoLinc = sh.VideoLinc,
              Title = sh.Title,
              AuthorId = sh.AuthorId,
              Author = new Author(){Id = sh.Author.Id,Name = sh.Author.Name,ImageLinc = sh.Author.ImageLinc},
              CategoryId = sh.CategoryId,
              Category = new Category(){Id = sh.Category.Id,Title = sh.Category.Title,ImageLinc = sh.Category.ImageLinc},
              HashtagId = sh.HashtagId
          })
          .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in articles)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return articles;
    }

    public async ValueTask<IList<ShortVideo>> GetShortVideoByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId)
    {
        var articles = await _shortVideoRepository
          .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
          .Where(x => x.CategoryId == categoryId)
          .Skip(metaQuery.Skip)
          .Take(metaQuery.Take)
          .Select(sh => new ShortVideo()
          {
              Id = sh.Id,
              VideoLinc = sh.VideoLinc,
              Title = sh.Title,
              AuthorId = sh.AuthorId,
              Author = new Author(){Id = sh.Author.Id,Name = sh.Author.Name,Content = sh.Author.Content,ImageLinc = sh.Author.ImageLinc},
              CategoryId = sh.CategoryId,
              Category = new Category(){Id = sh.Category.Id,Title = sh.Category.Title,Description = sh.Category.Description,ImageLinc = sh.Category.ImageLinc},
              HashtagId = sh.HashtagId
          })
          .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in articles)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return articles;
    }

    public async ValueTask<IList<ShortVideo>> GetShortVideoByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
       
        var filteredItems = await _shortVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.HashtagId.Any(y => y == hashtagId))
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(sh => new ShortVideo()
            {
                Id = sh.Id,
                VideoLinc = sh.VideoLinc,
                Title = sh.Title,
                AuthorId = sh.AuthorId,
                Author = new Author(){Id = sh.Author.Id,Name = sh.Author.Name,ImageLinc = sh.Author.ImageLinc},
                CategoryId = sh.CategoryId,
                Category = new Category(){Id = sh.Category.Id,Title = sh.Category.Title,ImageLinc = sh.Category.ImageLinc},
                HashtagId = sh.HashtagId
            })
            .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in filteredItems)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return filteredItems;
    }

    public async ValueTask<IList<ShortVideoForWithDetailsDto>> GetShortVideoWithDetailsAsync( MetaQueryModel metaQuery)
    {
        var shortVideos = await _shortVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(sh => new ShortVideo()
            {
                Id = sh.Id,
                VideoLinc = sh.VideoLinc,
                Title = sh.Title,
                AuthorId = sh.AuthorId,
                Author = new Author(){Id = sh.Author.Id,Name = sh.Author.Name,Content = sh.Author.Content,ImageLinc = sh.Author.ImageLinc},
                CategoryId = sh.CategoryId,
                Category = new Category(){Id = sh.Category.Id,Title = sh.Category.Title,Description = sh.Category.Description,ImageLinc = sh.Category.ImageLinc},
                HashtagId = sh.HashtagId
            })
            .ToListAsync();

        var result = shortVideos.Select(x => new ShortVideoForWithDetailsDto
        {
            id = x.Id,
            title = x.Title,
            author = x.Author,
            category = x.Category,
            videoLinc = x.VideoLinc,
            hashtagId = x.HashtagId,
            hashtags = new List<Hashtag>()
        }).ToList();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable()
            .ToListAsync();

        var hashtagsDic = hashtags.ToDictionary(hashtag => hashtag.Id);

        foreach (var res in result)
        {
            foreach (var value in from item in res.hashtagId where hashtagsDic.ContainsKey(item) select hashtagsDic[item])
            {
                res.hashtags.Add(value);
            }
        }

        return result;
    }

    public async ValueTask<ShortVideo> UpdateShortVideoAsync(ShortVideo article)
    {
        var articleResult = await _shortVideoRepository.GetByIdAsync(article.Id)
           ?? throw new NotFoundException("Logotype not found");

        articleResult.Title = article.Title ?? articleResult.Title;
        articleResult.VideoLinc = article.VideoLinc ?? articleResult.VideoLinc;
        articleResult.CategoryId = article.CategoryId != 0 ? article.CategoryId : articleResult.CategoryId;
        articleResult.AuthorId = article.AuthorId != 0 ? article.AuthorId : articleResult.AuthorId;
        articleResult.HashtagId = article.HashtagId ?? articleResult.HashtagId;

        return await _shortVideoRepository.UpdateAsync(articleResult);
    }
}
