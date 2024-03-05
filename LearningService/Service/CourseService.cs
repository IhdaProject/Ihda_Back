using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IHashtagRepository _hashtagRepository;
    public CourseService(ICourseRepository courseRepository,
        IHashtagRepository hashtagRepository)
    {
        _courseRepository = courseRepository;
        _hashtagRepository = hashtagRepository;
    }

    public async ValueTask<Course> CreateCourseAsync(CourseDto courseDto)
    {
        var newCourse = new Course()
        {
            Title = courseDto.title,
            AuthorId = courseDto.authorId,
            CategoryId = courseDto.categoryId,
            Description = courseDto.description,
            DocsUrl = courseDto.docsUrl,
            Image = courseDto.image,
            OrderNumber = courseDto.orderNumber,
            HashtagId = courseDto.hashtagId
        };

        return await _courseRepository.AddAsync(newCourse);
    }

    public async ValueTask<Course> DeleteCourseAsync(int courseId)
    {
        var courseResult = await _courseRepository.GetByIdAsync(courseId)
            ?? throw new NotFoundException("Not found");

        return await _courseRepository.RemoveAsync(courseResult);
    }

    public async ValueTask<IList<Course>> GetAllCourseAsync(MetaQueryModel metaQuery)
    {
        var courses = await _courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .OrderBy(c => c.OrderNumber)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                OrderNumber = c.OrderNumber,
                AuthorId = c.AuthorId,
                Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLinc = c.Author.ImageLinc},
                CategoryId = c.CategoryId,
                Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLinc = c.Category.ImageLinc},
                Quiz = new Quiz(){Id = c.Quiz.Id,Title = c.Quiz.Title,Description = c.Quiz.Description,Duration = c.Quiz.Duration,TotalScore = c.Quiz.TotalScore,PassingScore = c.Quiz.PassingScore,Heart = c.Quiz.Heart},
                DocsUrl = c.DocsUrl,
                HashtagId = c.HashtagId
            })
            .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in courses)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return courses;
    }
    
    public async ValueTask<IList<Course>> GetAllCourseByCategoryIdAsync(MetaQueryModel metaQuery,int categoryId)
    {
        var courses = await _courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .OrderBy(c => c.OrderNumber)
            .Where(c => c.CategoryId == categoryId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                OrderNumber = c.OrderNumber,
                AuthorId = c.AuthorId,
                Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLinc = c.Author.ImageLinc},
                CategoryId = c.CategoryId,
                Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLinc = c.Category.ImageLinc},
                Quiz = new Quiz(){Id = c.Quiz.Id,Title = c.Quiz.Title,Description = c.Quiz.Description,Duration = c.Quiz.Duration,TotalScore = c.Quiz.TotalScore,PassingScore = c.Quiz.PassingScore,Heart = c.Quiz.Heart},
                DocsUrl = c.DocsUrl,
                HashtagId = c.HashtagId
            })
            .ToListAsync();
        
        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);
        
        foreach (var res in courses)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return courses;
    }

    public async ValueTask<IList<Course>> GetAllCourseByAuthorIdAsync(MetaQueryModel metaQuery, int authorId)
    {
        var newCourse = await _courseRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .OrderBy(c => c.OrderNumber)
           .Where(x => x.AuthorId == authorId)
           .Skip(metaQuery.Skip)
           .Select(c => new Course()
           {
               Id = c.Id,
               Title = c.Title,
               Description = c.Description,
               Image = c.Image,
               OrderNumber = c.OrderNumber,
               AuthorId = c.AuthorId,
               Author = new Author() { Id = c.Author.Id, Name = c.Author.Name, ImageLinc = c.Author.ImageLinc },
               CategoryId = c.CategoryId,
               Category = new Category() { Id = c.Category.Id, Title = c.Category.Title, ImageLinc = c.Category.ImageLinc },
               Quiz = new Quiz(){Id = c.Quiz.Id,Title = c.Quiz.Title,Description = c.Quiz.Description,Duration = c.Quiz.Duration,TotalScore = c.Quiz.TotalScore,PassingScore = c.Quiz.PassingScore,Heart = c.Quiz.Heart},
               DocsUrl = c.DocsUrl,
               HashtagId = c.HashtagId
           })
           .Take(metaQuery.Take)
           .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newCourse)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }
        return newCourse;
    }

    public async ValueTask<IList<Course>> GetAllCourseByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
        var newCourse = await _courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .OrderBy(c => c.OrderNumber)
            .Where(x => x.HashtagId.Any(y => y == hashtagId))
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                OrderNumber = c.OrderNumber,
                AuthorId = c.AuthorId,
                Author = new Author() { Id = c.Author.Id, Name = c.Author.Name,  ImageLinc = c.Author.ImageLinc },
                CategoryId = c.CategoryId,
                Category = new Category() { Id = c.Category.Id, Title = c.Category.Title, ImageLinc = c.Category.ImageLinc },
                Quiz = new Quiz(){Id = c.Quiz.Id,Title = c.Quiz.Title,Description = c.Quiz.Description,Duration = c.Quiz.Duration,TotalScore = c.Quiz.TotalScore,PassingScore = c.Quiz.PassingScore,Heart = c.Quiz.Heart},
                DocsUrl = c.DocsUrl,
                HashtagId = c.HashtagId
            })
            .ToListAsync();

        var hashtags = await _hashtagRepository
            .GetAllAsQueryable(true)
            .ToDictionaryAsync(ht => ht.Id);

        foreach (var res in newCourse)
        {
            if (res.HashtagId is null) continue;
            foreach (var item in res.HashtagId.Where(item => hashtags.ContainsKey(item)))
            {
                res.Hashtags?.Add(hashtags[item]);
            }
        }

        return newCourse;
    }

    public async ValueTask<Course> GetCourseByIdAsync(int id)
    {
        var courseResult = await _courseRepository
                .GetAllAsQueryable(true)
                .Select(c => new Course()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Image = c.Image,
                    OrderNumber = c.OrderNumber,
                    AuthorId = c.AuthorId,
                    Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLinc = c.Author.ImageLinc},
                    CategoryId = c.CategoryId,
                    Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLinc = c.Category.ImageLinc},
                    Quiz = new Quiz(){Id = c.Quiz.Id,Title = c.Quiz.Title,Description = c.Quiz.Description,Duration = c.Quiz.Duration,TotalScore = c.Quiz.TotalScore,PassingScore = c.Quiz.PassingScore,Heart = c.Quiz.Heart},
                    DocsUrl = c.DocsUrl,
                    HashtagId = c.HashtagId
                })
                .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Not found");

        courseResult.Hashtags = await _hashtagRepository.GetAllAsQueryable(true)
            .Where(ht => courseResult.HashtagId.Contains(ht.Id))
            .ToListAsync();
        
         return courseResult;
    }

    public async ValueTask<Course> UpdateCourseAsync(Course course)
    {
        var courseResult = await _courseRepository.GetByIdAsync(course.Id)
            ?? throw new NotFoundException("Not found");

        courseResult.Title = course.Title ?? courseResult.Title;
        courseResult.Description = course.Description ?? courseResult.Description;
        courseResult.DocsUrl = course.DocsUrl ?? courseResult.DocsUrl;
        courseResult.Image = course.Image ?? courseResult.Image;
        courseResult.AuthorId = (course.AuthorId != 0) ? course.AuthorId : courseResult.AuthorId;
        courseResult.OrderNumber = (course.OrderNumber != 0) ? course.OrderNumber : courseResult.OrderNumber;
        courseResult.CategoryId = (course.CategoryId != 0) ? course.CategoryId : courseResult.CategoryId;
        courseResult.HashtagId = (course.HashtagId.Count != 0) ? course.HashtagId : courseResult.HashtagId;
        
        return await _courseRepository.UpdateAsync(courseResult);
    }
}
