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

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles.ToList();
        }
    }
}
