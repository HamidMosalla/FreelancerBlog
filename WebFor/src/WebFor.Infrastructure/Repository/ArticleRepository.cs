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
    public class ArticleRepository : IArticleRepository
    {
        private WebForDbContext _context;

        public ArticleRepository(WebForDbContext context)
        {
            _context = context;
        }

        public void Add(Article entity)
        {
            _context.Articles.Add(entity);
        }

        public void Remove(Article entity)
        {
            _context.Articles.Remove(entity);
        }
        
        public Article FindById(int id)
        {
            return _context.Articles.Single(a => a.ArticleId == id);
        }

        public Task<Article> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles.ToList();
        }

        public Task<List<Article>> GetAllAsync()
        {
            return _context.Articles.ToListAsync();
        }
    }
}
