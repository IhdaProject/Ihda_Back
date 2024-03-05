using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IHashtagRepository _hashtagRepository;

    public ArticleService(IArticleRepository articleRepository, 
        IHashtagRepository hashtagRepository)
    {
        _articleRepository = articleRepository;
        _hashtagRepository = hashtagRepository;
    }

    public async ValueTask<Article> CreateArticleAsync(ArticleDto article)
    {
        var newArticle = new Article()
        {
            Title = article.title,
            Description = article.description,
            Content = article.content,
            Image = article.image,
            AuthorId =article.authorId,
            CategoryId = article.categoryId,
            HashtagId = article.hashtagId,
        };

        return await _articleRepository.AddAsync(newArticle);
    }

    public async ValueTask<Article> DeleteArticleAsync(int articleId)
    {
        var articleResult = await _articleRepository.GetByIdAsync(articleId)
            ?? throw new NotFoundException("Logotype not found");

        return await _articleRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<Article>> GetAllArticleAsync(MetaQueryModel metaQuery)
    {
        var articles = await _articleRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(a => new Article()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                Image = a.Image,
                AuthorId = a.AuthorId,
                Author = new Author(){Id = a.Author.Id,Name = a.Author.Name,ImageLinc = a.Author.ImageLinc,Content = a.Author.Content},
                CategoryId = a.CategoryId,
                Category = new Category(){Id = a.Category.Id,Description = a.Category.Description,Title = a.Category.Title,ImageLinc = a.Category.ImageLinc},
                HashtagId = a.HashtagId
            })
            .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in articles)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return articles;
    }

    public async ValueTask<IList<Article>> GetAllArticleByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId)
    {
        var articles = await _articleRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(a => a.CategoryId == categoryId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(a => new Article()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                Image = a.Image,
                AuthorId = a.AuthorId,
                Author = new Author(){Id = a.Author.Id,Name = a.Author.Name,ImageLinc = a.Author.ImageLinc,Content = a.Author.Content},
                CategoryId = a.CategoryId,
                Category = new Category(){Id = a.Category.Id,Description = a.Category.Description,Title = a.Category.Title,ImageLinc = a.Category.ImageLinc},
                HashtagId = a.HashtagId
            })
            .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in articles)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return articles;
    }

    public async ValueTask<IList<ArticleForWithDetailsDto>> GetArticleWithDetailsAsync(MetaQueryModel metaQuery)
    {
        var articles = _articleRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(a => new Article()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                Image = a.Image,
                AuthorId = a.AuthorId,
                Author = new Author(){Id = a.Author.Id,Name = a.Author.Name,ImageLinc = a.Author.ImageLinc,Content = a.Author.Content},
                CategoryId = a.CategoryId,
                Category = new Category(){Id = a.Category.Id,Description = a.Category.Description,Title = a.Category.Title,ImageLinc = a.Category.ImageLinc},
                HashtagId = a.HashtagId
            })
            .ToList();

        var result = articles.Select(x => new ArticleForWithDetailsDto()
        {
            id = x.Id,
            title = x.Title,
            content = x.Content,
            image = x.Image,
            author = x.Author,
            category = x.Category,
            hashtags = new List<Hashtag>(),
            hashtagIds =x.HashtagId
        }).ToList();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable()
            .ToListAsync();

        Dictionary<int,Hashtag> hashtagsDic = new Dictionary<int,Hashtag>();
        foreach (var hashtag in hashtags) 
        {
            hashtagsDic.Add(hashtag.Id, hashtag);
        }

        foreach (var res in result)
        {
            foreach(var item in res.hashtagIds)
            {
                if (hashtagsDic.ContainsKey(item))
                {
                    Hashtag value = hashtagsDic[item];
                    res.hashtags.Add(value);
                }
            }
        }

        return result;
    }
    
    public async ValueTask<Article> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);

        return article;
    }

    public async ValueTask<Article> UpdateArticleAsync(Article article)
    {
        var articleResult = await _articleRepository.GetByIdAsync(article.Id)
            ?? throw new NotFoundException("Not found");

        articleResult.Title = article.Title ?? articleResult.Title;
        articleResult.Content = article.Content ?? articleResult.Content;
        articleResult.Description = article.Description ?? articleResult.Description;
        articleResult.Image = article.Image ?? articleResult.Image;
        articleResult.HashtagId = article.HashtagId.Count is not 0 ? article.HashtagId : articleResult.HashtagId;
        articleResult.CategoryId = article.CategoryId is not 0 ? article.CategoryId : articleResult.CategoryId;
        articleResult.AuthorId = article.AuthorId is not 0 ? article.AuthorId : articleResult.AuthorId;

        return await _articleRepository.UpdateAsync(articleResult);
    }

    public async ValueTask<IList<Article>> GetAllArticleByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
        var newArticle = await _articleRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.HashtagId.Any(y => y == hashtagId))
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(a => new Article()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                Image = a.Image,
                AuthorId = a.AuthorId,
                Author = new Author() { Id = a.Author.Id, Name = a.Author.Name, ImageLinc = a.Author.ImageLinc},
                CategoryId = a.CategoryId,
                Category = new Category() { Id = a.Category.Id,  Title = a.Category.Title, ImageLinc = a.Category.ImageLinc },
                HashtagId = a.HashtagId
            })
            .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newArticle)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }



        return newArticle;
    }

    public async ValueTask<IList<Article>> GetAllArticleByAuthorIdAsync(MetaQueryModel metaQuery, int authorId)
    {
        var newArticle = await _articleRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.AuthorId == authorId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(a => new Article()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                Image = a.Image,
                AuthorId = a.AuthorId,
                Author = new Author() { Id = a.Author.Id, Name = a.Author.Name, ImageLinc = a.Author.ImageLinc },
                CategoryId = a.CategoryId,
                Category = new Category() { Id = a.Category.Id, Title = a.Category.Title, ImageLinc = a.Category.ImageLinc },
                HashtagId = a.HashtagId
            })
            .ToListAsync();
        var hashtags = await _hashtagRepository
             .GetAllAsQueryable(true)
             .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newArticle)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }
        return newArticle;
    }
}
