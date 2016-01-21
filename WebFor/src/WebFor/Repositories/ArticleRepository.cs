using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using WebFor.Models;

namespace WebFor.Repositories
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

        public void AddAsync(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Article entity)
        {
            _context.Articles.Remove(entity);
        }

        public void RemoveAsync(Article entity)
        {
            throw new NotImplementedException();
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
