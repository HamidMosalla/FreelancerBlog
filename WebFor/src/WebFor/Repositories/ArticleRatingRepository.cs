using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class ArticleRatingRepository:IArticleRatingRepository
    {
        private ApplicationDbContext _context;

        public ArticleRatingRepository(ApplicationDbContext context)
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

        public IEnumerable<ArticleRating> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
