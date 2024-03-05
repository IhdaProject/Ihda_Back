using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IShortVideoService
{
    ValueTask<ShortVideo> CreateShortVideoAsync(ShortVideoDto article);
    ValueTask<ShortVideo> UpdateShortVideoAsync(ShortVideo article);
    ValueTask<ShortVideo> DeleteShortVideoAsync(int articleId);
    ValueTask<IList<ShortVideo>> GetShortVideoByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId);
    ValueTask<IList<ShortVideoForWithDetailsDto>> GetShortVideoWithDetailsAsync(MetaQueryModel metaQuery);
    ValueTask<IList<ShortVideo>> GetShortVideoByAftorIdAsync(MetaQueryModel metaQuery, int aftorId);
    ValueTask<IList<ShortVideo>> GetShortVideoByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId);
    ValueTask<IList<ShortVideo>> GetAllShortVideoAsync(MetaQueryModel metaQuery);
}
