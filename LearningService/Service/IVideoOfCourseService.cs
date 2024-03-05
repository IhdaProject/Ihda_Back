using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IVideoOfCourseService
{
    ValueTask<VideoOfCourse> CreateVideoOfCourseAsync(VideosOfCourseDto videoOfCourse);
    ValueTask<IList<VideoOfCourse>> GetAllVideoOfCourseAsync(MetaQueryModel metaQuery);
    ValueTask<IList<VideoOfCourse>> GetVideoOfCourseByCourseIdAsync(MetaQueryModel metaQuery, int courseId);
    ValueTask<VideoOfCourse> GetVideoOfCourseByIdAsync(int id);
    ValueTask<VideoOfCourse> UpdateVideoOfCourseAsync(VideoOfCourse videoOfCourse);
    ValueTask<VideoOfCourse> DeleteVideoOfCourseAsync(int id);
}
