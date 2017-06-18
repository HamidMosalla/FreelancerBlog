using System;
using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Infrastructure.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly WebForDbContext _context;

        public UnitOfWork(WebForDbContext context)
        {
            _context = context;
        }

        private IArticleRepository _articleRepository;
        private IUserRepository _userRepository;
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

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
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
