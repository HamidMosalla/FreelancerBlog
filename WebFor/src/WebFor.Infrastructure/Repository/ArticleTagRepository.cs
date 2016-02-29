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
            _context.ArticleTags.Add(entity);
        }

        public void Remove(ArticleTag entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleTag entity)
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

        public void AddRange(List<ArticleTag> tagList)
        {
            _context.ArticleTags.AddRange(tagList);
        }

        public async Task<string> GetTagsByArticleIdAsync(int articleId)
        {
            var listOfArticleTags = await _context.ArticleArticleTags.Where(a => a.ArticleId == articleId).ToListAsync();

            var arrayOfTags = listOfArticleTags.Join/* unnecessary, here just for reference <ArticleArticleTag, ArticleTag, int, string>*/(await _context.ArticleTags.ToListAsync(), a => a.ArticleTagId, t => t.ArticleTagId,
                (a, t) => t.ArticleTagName).ToArray();

            return string.Join(",", arrayOfTags);
        }

        public Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> exept)
        {
            return _context.ArticleTags.Join(exept, a => a.ArticleTagName, e => e, (a, e) => a).ToListAsync();
        }

        public Task<int> RemoveRange(List<ArticleTag> tagsToRemove)
        {
            _context.ArticleTags.RemoveRange(tagsToRemove);
            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId)
        {
            var listOfaATags = _context.ArticleArticleTags.Join(tagsToRemove, a => a.ArticleTagId, t => t.ArticleTagId, (a, t) => a).Where(c => c.ArticleId == articleId).ToList();

            _context.ArticleArticleTags.RemoveRange(listOfaATags);

            return _context.SaveChangesAsync();
        }
    }
}
