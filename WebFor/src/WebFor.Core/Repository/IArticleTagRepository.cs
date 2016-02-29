using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleTagRepository:IRepository<ArticleTag, int>
    {
        Task<string[]> GetAllTagNamesArrayAsync();
        void AddRange(List<ArticleTag> tagList);

        Task<string> GetTagsByArticleIdAsync(int articleId);
        Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> exept);
        Task<int> RemoveRange(List<ArticleTag> tagsToRemove);

        Task<int> RemoveRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId);
    }
}
