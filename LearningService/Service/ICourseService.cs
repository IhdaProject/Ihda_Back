using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service
{
    public interface ICourseService
    {
        ValueTask<Course> CreateCourseAsync(CourseDto courseDto);
        ValueTask<Course> UpdateCourseAsync(Course course);
        ValueTask<Course> DeleteCourseAsync(int courseId);
        ValueTask<Course> GetCourseByIdAsync(int id);
        ValueTask<IList<Course>> GetAllCourseAsync(MetaQueryModel metaQuery);
        ValueTask<IList<Course>> GetAllCourseByAuthorIdAsync(MetaQueryModel metaQuery,int authorId);
        ValueTask<IList<Course>> GetAllCourseByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId);
        ValueTask<IList<Course>> GetAllCourseByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId);
    }
}