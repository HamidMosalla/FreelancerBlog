using System;

namespace WebFor.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository ArticleRepository { get; }
        IUserRepository UserRepository { get; }
        IArticleCommentRepository ArticleCommentRepository { get; }
        IArticleRatingRepository ArticleRatingRepository { get; }
        IArticleTagRepository ArticleTagRepository { get; }
        IContactRepository ContactRepository { get; }
        IPortfolioRepository PortfolioRepository { get; }
        ISiteOrderRepository SiteOrderRepository { get; }
        ISlideShowRepository SlideShowRepository { get; }
        void Save();
        new void Dispose();
    }
}