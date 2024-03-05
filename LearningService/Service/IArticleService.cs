using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IArticleService
{ 
    ValueTask<Article> CreateArticleAsync(ArticleDto article);
    ValueTask<Article> UpdateArticleAsync(Article article);
    ValueTask<Article> DeleteArticleAsync(int articleId);
    ValueTask<Article> GetArticleByIdAsync(int id);
    ValueTask<IList<Article>> GetAllArticleAsync(MetaQueryModel metaQuery);
    ValueTask<IList<Article>> GetAllArticleByHashtagIdAsync(MetaQueryModel metaQuery,int hashtagId);
    ValueTask<IList<Article>> GetAllArticleByAuthorIdAsync(MetaQueryModel metaQuery,int authorId);
    ValueTask<IList<Article>> GetAllArticleByCategoryIdAsync(MetaQueryModel metaQuery,int categoryId);
    ValueTask<IList<ArticleForWithDetailsDto>> GetArticleWithDetailsAsync(MetaQueryModel metaQuery);
}