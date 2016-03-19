using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleRepository : IRepository<Article, int>
    {
        Task<int> UpdateArticleAsync(Article article);

        Task<int> DeleteArticleAsync(Article article);
        Task<List<ArticleTag>> GetCurrentArticleTagsAsync(int articleId);

        Task<int> IncreaseArticleViewCount(int articleId);
    }
}
