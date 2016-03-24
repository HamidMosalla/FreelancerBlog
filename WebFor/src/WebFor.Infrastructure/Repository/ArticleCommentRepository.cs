using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.ArticleComments.SingleAsync(a => a.ArticleCommentId.Equals(id));
        }

        public IEnumerable<ArticleComment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleComment>> GetAllAsync()
        {
            return _context.ArticleComments.OrderBy(a => a.IsCommentApproved).ThenByDescending(a => a.ArticleCommentDateCreated).Include(a => a.Article).Include(a => a.ApplicationUser).ToListAsync();
        }

        public Task<int> AddCommentToArticle(ArticleComment articleComment)
        {
            _context.ArticleComments.Add(articleComment);

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteArticleCommentAsync(ArticleComment model)
        {
            _context.ArticleComments.Remove(model);
            return _context.SaveChangesAsync();
        }

        public Task<int> ToggleArticleCommentApproval(ArticleComment model)
        {
            model.IsCommentApproved = !model.IsCommentApproved;

            return _context.SaveChangesAsync();
        }

        public Task<int> EditArticleCommentAsync(ArticleComment model, string newCommentBody)
        {
            model.ArticleCommentBody = newCommentBody;
            return _context.SaveChangesAsync();
        }
    }
}
