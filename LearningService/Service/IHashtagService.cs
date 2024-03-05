using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IHashtagService
{
    ValueTask<Hashtag> CreateHashtagAsync(HashtagDto article);
    ValueTask<Hashtag> UpdateHashtagAsync(Hashtag article);
    ValueTask<Hashtag> DeleteHashtagAsync(int articleId);
    ValueTask<Hashtag> GetHashtagByIdAsync(int id);
    ValueTask<IList<Hashtag>> GetAllHashtagAsync(MetaQueryModel metaQuery);
    ValueTask<GetByHashtagIdDto> GetAllByHashtagId(int hashtagId, string category, MetaQueryModel metaQuery);
}
