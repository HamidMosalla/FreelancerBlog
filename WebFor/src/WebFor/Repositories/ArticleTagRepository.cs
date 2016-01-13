using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class ArticleTagRepository : IArticleTagRepository
    {
        private ApplicationDbContext _context;

        public ArticleTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(ArticleTag entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleTag entity)
        {
            throw new NotImplementedException();
        }

        public ArticleTag FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleTag> GetAll(Func<ArticleTag, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
