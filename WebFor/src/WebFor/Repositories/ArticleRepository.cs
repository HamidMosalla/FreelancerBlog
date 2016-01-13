using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Article entity)
        {
            throw new NotImplementedException();
        }

        public Article FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll(Func<Article, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
