using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface ISeminarVideoService
{
    ValueTask<SeminarVideo> CreateSeminarVideoAsync(SeminarVideoDto seminarVideo);
    ValueTask<Category> CreateSeminarVideoCategoryAsync(CategoryDto seminarVideoCategory);
    ValueTask<IList<SeminarVideo>> GetSeminarVideoByCategoryIdAsync( MetaQueryModel metaQuery,int categoryId);
    ValueTask<IList<SeminarVideoForWhithDetaileDto>> GetSeminarVideoWithDetailsAsync(MetaQueryModel metaQuery);
    ValueTask<IList<SeminarVideo>> GetAllSeminarVideoAsync( MetaQueryModel metaQuery);
    ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByHashtagIdAsync( MetaQueryModel metaQuery,int hashtagId);
    ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByAuthorIdAsync( MetaQueryModel metaQuery, int authorId);
    ValueTask<IList<Category>> GetAllSeminarVideoCategoryAsync(MetaQueryModel metaQuery);
    ValueTask<Category> GetCategoryById(int id);
    ValueTask<SeminarVideo> UpdateSeminarVideoAsync(SeminarVideo seminarVideo);
    ValueTask<Category> UpdateSeminarVideoCategoryAsync(Category seminarVideoCategory);
    ValueTask<SeminarVideo> DeleteSeminarVideoAsync(int seminarVideoId);
    ValueTask<Category>  DeleteSeminarVideoCategoryAsync(int categoryId);
}