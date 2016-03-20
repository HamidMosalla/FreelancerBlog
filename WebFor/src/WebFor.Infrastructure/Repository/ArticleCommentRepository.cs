using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
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

        public void Remove(ArticleComment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleComment entity)
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

        public Task<int> AddCommentToArticle(ArticleComment articleComment)
        {
            _context.ArticleComments.Add(articleComment);

            return _context.SaveChangesAsync();
        }
    }
}
