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
    public class ArticleArticleTagRepository : IArticleArticleTagRepository
    {

        private WebForDbContext _context;

        public ArticleArticleTagRepository(WebForDbContext context)
        {
            _context = context;
        }

        public void Add(ArticleArticleTag entity)
        {
            _context.ArticleArticleTags.Add(entity);
        }

        public void Remove(ArticleArticleTag entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleArticleTag entity)
        {
            throw new NotImplementedException();
        }

        public ArticleArticleTag FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleArticleTag> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleArticleTag> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleArticleTag>> GetAllAsync()
        {
            return _context.ArticleArticleTags.ToListAsync();
        }

        public void AddRange(List<ArticleArticleTag> joinTableArtTagList)
        {
            _context.ArticleArticleTags.AddRange(joinTableArtTagList);
        }
    }
}
