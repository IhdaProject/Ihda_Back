using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class SeminarVideoService : ISeminarVideoService
{
    private readonly ISeminarVideoRepository _seminarVideoRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHashtagRepository _hashtagRepository;

    public SeminarVideoService(
        ICategoryRepository categoryRepository,
        ISeminarVideoRepository seminarVideoRepository,
        IHashtagRepository hashtagRepository)
    {
        _categoryRepository = categoryRepository;
        _seminarVideoRepository = seminarVideoRepository;
        _hashtagRepository = hashtagRepository;
    }

    public async ValueTask<SeminarVideo> CreateSeminarVideoAsync(SeminarVideoDto seminarVideo)
    {
        var nwCourse = new SeminarVideo()
        {
            Title = seminarVideo.title,
            VideoLinc = seminarVideo.videoLinc,
            AuthorId =seminarVideo.authorId,
            HashtagId = seminarVideo.hashtagId,
            CategoryId = seminarVideo.categoryId,
        };

        return await _seminarVideoRepository.AddAsync(nwCourse);
    }

    public async ValueTask<Category> CreateSeminarVideoCategoryAsync(CategoryDto seminarVideoCategory)
    {
        var nwCategory = new Category() 
        {        
            Title = seminarVideoCategory.title,
            Description =seminarVideoCategory.description,
            ImageLinc = seminarVideoCategory.imageLinc
        };
        return await _categoryRepository.AddAsync(nwCategory);
    }

    public async ValueTask<SeminarVideo> DeleteSeminarVideoAsync(int seminarVideoId)
    {
        var articleResult = await _seminarVideoRepository.GetByIdAsync(seminarVideoId)
            ?? throw new NotFoundException("Logotype not found");

        return await _seminarVideoRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<Category> DeleteSeminarVideoCategoryAsync(int categoryId)
    {
        var articleResult = await _categoryRepository.GetByIdAsync(categoryId)
                            ?? throw new NotFoundException("Logotype not found");

        return await _categoryRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoAsync(MetaQueryModel metaQuery)
    {
        var seminarVideos = await _seminarVideoRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .Skip(metaQuery.Skip)
           .Take(metaQuery.Take)
           .Select(sv => new SeminarVideo()
           {
               Id = sv.Id,
               CategoryId = sv.CategoryId,
               VideoLinc = sv.VideoLinc,
               Title = sv.Title,
               AuthorId = sv.AuthorId,
               HashtagId = sv.HashtagId,
               Category = new Category(){Id = sv.Category!.Id,Title = sv.Category.Title,ImageLinc = sv.Category.ImageLinc,Description = sv.Category.Description},
               Author = new Author(){Id = sv.Author!.Id,Name = sv.Author.Name,ImageLinc = sv.Author.ImageLinc}
           })
           .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in seminarVideos)
        {
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return seminarVideos;
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByAuthorIdAsync(MetaQueryModel metaQuery, int authorId)
    {
        var newSeminars = await _seminarVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.AuthorId == authorId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(seminar => new SeminarVideo()
            {
                Id = seminar.Id,
                VideoLinc = seminar.VideoLinc,
                Title = seminar.Title,
                AuthorId = seminar.AuthorId,
                Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, ImageLinc = seminar.Author.ImageLinc  },
                CategoryId = seminar.CategoryId,
                Category = new Category() { Id = seminar.Category.Id, Title = seminar.Category.Title, ImageLinc = seminar.Category.ImageLinc },
                HashtagId = seminar.HashtagId

            })
            .ToListAsync();

        var hashtags = await _hashtagRepository
                .GetAllAsQueryable(true)
                .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newSeminars)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return newSeminars;
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
        var newSeminars = await _seminarVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.HashtagId.Any(y => y == hashtagId))
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(seminar => new SeminarVideo()
            {
                Id = seminar.Id,
                VideoLinc = seminar.VideoLinc,
                Title = seminar.Title,
                AuthorId = seminar.AuthorId,
                Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, ImageLinc = seminar.Author.ImageLinc },
                CategoryId = seminar.CategoryId,
                Category = new Category() { Id = seminar.Category.Id, Title = seminar.Category.Title, ImageLinc = seminar.Category.ImageLinc },
                HashtagId = seminar.HashtagId

            })
            .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newSeminars)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return newSeminars;
    }

    public async ValueTask<IList<Category>> GetAllSeminarVideoCategoryAsync(MetaQueryModel metaQuery)
    {
        var articles = await _categoryRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .ToListAsync();
        return articles;
    }

    public async ValueTask<Category> GetCategoryById(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async ValueTask<IList<SeminarVideo>> GetSeminarVideoByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId)
    {
        var seminars = await _seminarVideoRepository
             .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
             .Where(vc => vc.CategoryId == categoryId)
             .Skip(metaQuery.Skip)
             .Take(metaQuery.Take)
             .Select(seminar => new SeminarVideo()
             {
                 Id = seminar.Id,
                 VideoLinc = seminar.VideoLinc,
                 Title = seminar.Title,
                 AuthorId = seminar.AuthorId,
                 Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, Content = seminar.Author.Content, ImageLinc = seminar.Author.ImageLinc },
                 CategoryId = seminar.CategoryId,
                 Category = new Category() { Id = seminar.Category.Id,  Title = seminar.Category.Title, Description = seminar.Category.Description, ImageLinc = seminar.Category.ImageLinc },
                 HashtagId = seminar.HashtagId

             })
             .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in seminars)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }
        return seminars;
    }

    public async ValueTask<IList<SeminarVideoForWhithDetaileDto>> GetSeminarVideoWithDetailsAsync(MetaQueryModel metaQuery)
    {
        var articles = _seminarVideoRepository
                .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
                .Skip(metaQuery.Skip)
                .Take(metaQuery.Take)
                .ToList();

        var result = articles.Select(x => new SeminarVideoForWhithDetaileDto()
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

    public async ValueTask<SeminarVideo> UpdateSeminarVideoAsync(SeminarVideo seminarVideo)
    {
        var result = await _seminarVideoRepository.GetByIdAsync(seminarVideo.Id)
           ?? throw new NotFoundException("Logotype not found");
        result.Title = seminarVideo.Title ?? result.Title;
        result.VideoLinc = seminarVideo.VideoLinc ?? result.VideoLinc;
        result.CategoryId = seminarVideo.CategoryId != 0 ? seminarVideo.CategoryId : result.CategoryId;
        result.AuthorId = seminarVideo.AuthorId != 0 ? seminarVideo.AuthorId : result.AuthorId;
        result.HashtagId = seminarVideo.HashtagId ?? result.HashtagId;

        return await _seminarVideoRepository .UpdateAsync(result);
    }

    public async ValueTask<Category> UpdateSeminarVideoCategoryAsync(Category seminarVideoCategory)
    {
        var result = await _categoryRepository.GetByIdAsync(seminarVideoCategory.Id)
                           ?? throw new NotFoundException("Logotype not found");

        result.Title = seminarVideoCategory?.Title ?? result.Title;
        result.Description = seminarVideoCategory.Description ?? result.Description;
        result.ImageLinc = seminarVideoCategory?.ImageLinc ?? result.ImageLinc;

        return await _categoryRepository .UpdateAsync(result);
    }
}
