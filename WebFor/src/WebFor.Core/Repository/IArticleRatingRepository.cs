using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleRatingRepository : IRepository<ArticleRating, int>
    {

        Task<int> AddRatingForArticle(int id, double rating, string userIDfk);

        bool IsRatedBefore(int id, string userIDfk);

        double CalculateArticleRating(int id);
        Task<ArticleRating> GetCurrentUserRating(int articleId, string userIDfk);
        Task<int> UpdateArticleRating(int id, double rating, string userIDfk);
        Task<int> GetNumberOfVoters(int articleId);
    }
}
