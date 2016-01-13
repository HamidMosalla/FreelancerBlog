using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class UnitOfWork : IDisposable
    {

        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private IArticleRepository _articleRepository;
        private IUserRepository _userRepository;
        private IArticleCommentRepository _articleCommentRepository;
        private IArticleRatingRepository _articleRatingRepository;
        private IArticleTagRepository _articleTagRepository;
        private IContactRepository _contactRepository;
        private IPortfolioRepository _portfolioRepository;
        private ISiteOrderRepository _siteOrderRepository;
        private ISlideShowRepository _slideShowRepository;

        public IArticleRepository ArticleRepository
        {
            get
            {

                if (this._articleRepository == null)
                {
                    this._articleRepository = new ArticleRepository(_context);
                }
                return _articleRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }
        public IArticleCommentRepository ArticleCommentRepository
        {
            get
            {

                if (this._articleCommentRepository == null)
                {
                    this._articleCommentRepository = new ArticleCommentRepository(_context);
                }
                return _articleCommentRepository;
            }
        }
        public IArticleRatingRepository ArticleRatingRepository
        {
            get
            {

                if (this._articleRatingRepository == null)
                {
                    this._articleRatingRepository = new ArticleRatingRepository(_context);
                }
                return _articleRatingRepository;
            }
        }
        public IArticleTagRepository ArticleTagRepository
        {
            get
            {

                if (this._articleTagRepository == null)
                {
                    this._articleTagRepository = new ArticleTagRepository(_context);
                }
                return _articleTagRepository;
            }
        }
        public IContactRepository ContactRepository
        {
            get
            {

                if (this._contactRepository == null)
                {
                    this._contactRepository = new ContactRepository(_context);
                }
                return _contactRepository;
            }
        }
        public IPortfolioRepository PortfolioRepository
        {
            get
            {

                if (this._portfolioRepository == null)
                {
                    this._portfolioRepository = new PortfolioRepository(_context);
                }
                return _portfolioRepository;
            }
        }
        public ISiteOrderRepository SiteOrderRepository
        {
            get
            {

                if (this._siteOrderRepository == null)
                {
                    this._siteOrderRepository = new SiteOrderRepository(_context);
                }
                return _siteOrderRepository;
            }
        }
        public ISlideShowRepository SlideShowRepository
        {
            get
            {

                if (this._slideShowRepository == null)
                {
                    this._slideShowRepository = new SlideShowRepository(_context);
                }
                return _slideShowRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
