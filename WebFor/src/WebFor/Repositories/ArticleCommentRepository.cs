using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        private ApplicationDbContext _context;

        public ArticleCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public ArticleComment FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleComment> GetAll(Func<ArticleComment, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
