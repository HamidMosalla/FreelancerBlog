using System;
using System.Threading.Tasks;

namespace WebFor.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository ArticleRepository { get; }
        IUserRepository UserRepository { get; }
        IArticleCommentRepository ArticleCommentRepository { get; }
        IArticleRatingRepository ArticleRatingRepository { get; }
        IArticleTagRepository ArticleTagRepository { get; }
        IArticleArticleTagRepository ArticleArticleTagRepository { get; }
        IContactRepository ContactRepository { get; }
        IPortfolioRepository PortfolioRepository { get; }
        ISiteOrderRepository SiteOrderRepository { get; }
        ISlideShowRepository SlideShowRepository { get; }
        int Save();
        Task<int> SaveAsync();
        new void Dispose();
    }
}