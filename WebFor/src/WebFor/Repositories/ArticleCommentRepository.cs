using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        private WebForDbContext _context;

        public ArticleCommentRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public void AddAsync(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public ArticleComment FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleComment> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleComment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleComment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
