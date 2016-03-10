using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleTagRepository:IRepository<ArticleTag, int>
    {
        Task<string[]> GetAllTagNamesArrayAsync();

        Task<int> AddRangeOfTags(IEnumerable<string> exceptAddedTags);

        Task<string> GetTagsByArticleIdAsync(int articleId);

        Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> delimitedTags);

        Task<int> RemoveRange(List<ArticleTag> tagsToRemove);

        Task<int> RemoveTagRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId);

        Task<int> AddTagRangeToArticle(List<ArticleTag> tagsToAdd, Article article);
    }
}
