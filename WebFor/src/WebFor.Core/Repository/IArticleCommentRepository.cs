using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleCommentRepository : IRepository<ArticleComment, int>
    {
        Task<int> AddCommentToArticle(ArticleComment articleComment);
        Task<int> DeleteArticleCommentAsync(ArticleComment model);
        Task<int> ToggleArticleCommentApproval(ArticleComment model);
        Task<int> EditArticleCommentAsync(ArticleComment model, string newCommentBody);
    }
}
