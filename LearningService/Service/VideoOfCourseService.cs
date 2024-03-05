using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class VideoOfCourseService : IVideoOfCourseService
{
    private readonly IVideoOfCourseRepository _videoOfCourseRepository;

    public VideoOfCourseService(IVideoOfCourseRepository videoOfCourseRepository)
    {
        _videoOfCourseRepository = videoOfCourseRepository;
    }

    public async ValueTask<VideoOfCourse> CreateVideoOfCourseAsync(VideosOfCourseDto videoOfCourse)
    {
        var  VideoOfCourse = new VideoOfCourse()
        {
            VideoLinc =  videoOfCourse.videoLinc,
            Title = videoOfCourse.title,
            Content = videoOfCourse.content,
            CourseId = videoOfCourse.courseId,
            OrderNumber = videoOfCourse.orderNumber,
            DocsUrl = videoOfCourse.docsUrl
        };

        return await _videoOfCourseRepository.AddAsync(VideoOfCourse);
    }

    public async ValueTask<VideoOfCourse> DeleteVideoOfCourseAsync(int id)
    {
        var articleResult = await _videoOfCourseRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Logotype not found");

        return await _videoOfCourseRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<VideoOfCourse>> GetAllVideoOfCourseAsync(MetaQueryModel metaQuery)
    {
        var articles = await _videoOfCourseRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .OrderBy(x => x.OrderNumber)
           .Skip(metaQuery.Skip)
           .Take(metaQuery.Take)
           .Select(voc => new VideoOfCourse()
           {
               Id = voc.Id,
               Content = voc.Content,
               CourseId = voc.CourseId,
               Title = voc.Title,
               OrderNumber = voc.OrderNumber,
               DocsUrl = voc.DocsUrl,
               VideoLinc = voc.VideoLinc,
               Course = new Course(){Id = voc.Course.Id, Title = voc.Course.Title, Description = voc.Course.Description}
           })
           .ToListAsync();

        return articles;
    }

    public async ValueTask<IList<VideoOfCourse>> GetVideoOfCourseByCourseIdAsync(MetaQueryModel metaQuery, int courseId)
    {
        var result = await _videoOfCourseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x  => x.CourseId == courseId)
            .OrderBy(x => x.OrderNumber)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(voc => new VideoOfCourse()
            {
                Id = voc.Id,
                Content = voc.Content,
                CourseId = voc.CourseId,
                Title = voc.Title,
                OrderNumber = voc.OrderNumber,
                DocsUrl = voc.DocsUrl,
                VideoLinc = voc.VideoLinc,
                Course = new Course(){Id = voc.Course.Id, Title = voc.Course.Title, Description = voc.Course.Description}
            })
            .ToListAsync();

        return result;
    }

    public async ValueTask<VideoOfCourse> GetVideoOfCourseByIdAsync(int id)
    {
        var result = await _videoOfCourseRepository
            .GetByIdAsync(id);

        return result;
    }

    public async ValueTask<VideoOfCourse> UpdateVideoOfCourseAsync(VideoOfCourse videoOfCourse)
    {
        var result = await _videoOfCourseRepository.GetByIdAsync(videoOfCourse.Id)
            ?? throw new NotFoundException("Logotype not found");

        result.VideoLinc = videoOfCourse.VideoLinc ?? result.VideoLinc;
        result.Title = videoOfCourse.Title ?? result.Title;
        result.Content = videoOfCourse.Content ?? result.Content;
        result.OrderNumber = videoOfCourse?.OrderNumber ?? result.OrderNumber;
        result.CourseId = videoOfCourse.CourseId != 0 ? videoOfCourse.CourseId : result.CourseId;
        result.DocsUrl = videoOfCourse.DocsUrl ?? result.DocsUrl;

        return await _videoOfCourseRepository.UpdateAsync(result);
    }
}
