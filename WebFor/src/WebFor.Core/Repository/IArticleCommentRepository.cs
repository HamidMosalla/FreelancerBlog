using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleCommentRepository : IRepository<ArticleComment, int>
    {
        Task<int> AddCommentToArticle(ArticleComment articleComment);
    }
}
