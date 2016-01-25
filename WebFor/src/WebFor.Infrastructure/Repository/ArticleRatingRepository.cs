using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class ArticleRatingRepository:IArticleRatingRepository
    {
        private WebForDbContext _context;

        public ArticleRatingRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public ArticleRating FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleRating> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleRating> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleRating>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
