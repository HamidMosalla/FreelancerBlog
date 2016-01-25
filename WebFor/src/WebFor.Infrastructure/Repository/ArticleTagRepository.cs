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
    public class ArticleTagRepository : IArticleTagRepository
    {
        private WebForDbContext _context;

        public ArticleTagRepository(WebForDbContext context)
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

        public Task<ArticleTag> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleTag> GetAll()
        {
            return _context.ArticleTags.ToList();
        }

        public Task<List<ArticleTag>> GetAllAsync()
        {
            return _context.ArticleTags.ToListAsync();
        }

        public Task<string[]> GetAllTagNamesArrayAsync()
        {
            return _context.ArticleTags.Select(a => a.ArticleTagName).ToArrayAsync();
        }
    }
}
