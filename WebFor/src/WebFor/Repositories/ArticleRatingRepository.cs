using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
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

        public void AddAsync(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(ArticleRating entity)
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
