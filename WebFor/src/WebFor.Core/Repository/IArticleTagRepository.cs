using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleTagRepository:IRepository<ArticleTag, int>
    {
        Task<string[]> GetAllTagNamesArrayAsync();
    }
}
