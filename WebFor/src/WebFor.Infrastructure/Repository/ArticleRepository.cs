﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Task<int> DeleteArticleAsync(Article article)
        {
            _context.Articles.Remove(article);
            return _context.SaveChangesAsync();
        }

        public void Update(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateArticleAsync(Article article)
        {
            _context.Articles.Attach(article);

            var entity = _context.Entry(article);
            entity.State = EntityState.Modified;

            entity.Property(e => e.ArticleDateCreated).IsModified = false;
            entity.Property(e => e.ArticleId).IsModified = false;
            entity.Property(e => e.ArticleViewCount).IsModified = false;
            entity.Property(e => e.UserIDfk).IsModified = false;

            return _context.SaveChangesAsync();
        }

        public Task<List<ArticleTag>> GetCurrentArticleTagsAsync(int articleId)
        {
            return _context.ArticleArticleTags.Where(a => a.ArticleId == articleId).Join(_context.ArticleTags.ToList(), aat => aat.ArticleTagId, at => at.ArticleTagId, (aat, at) => at).ToListAsync();
        }

        public async Task<int> IncreaseArticleViewCount(int articleId)
        {
            var article = await _context.Articles.SingleAsync(a => a.ArticleId == articleId);

            article.ArticleViewCount += 1;

            return await _context.SaveChangesAsync();
        }

        public Task<List<Article>> GetArticlesByTag(int tagId)
        {
            return _context.ArticleArticleTags.Where(a => a.ArticleTagId.Equals(tagId)).Join(_context.Articles.Include(a => a.ApplicationUser).Include(a => a.ArticleComments).Include(a => a.ArticleRatings), left => left.ArticleId, right => right.ArticleId, (aat, a) => a).ToListAsync();
        }

        public Task<List<Article>> GetLatestArticles(int numberOfArticles)
        {
            return _context.Articles.OrderByDescending(a => a.ArticleDateCreated).Take(numberOfArticles).ToListAsync();
        }

        public Article FindById(int id)
        {
            return _context.Articles.Include(a => a.ApplicationUser).Include(a => a.ArticleRatings).Include(a => a.ArticleComments).Single(a => a.ArticleId == id);
        }

        public Task<Article> FindByIdAsync(int id)
        {
            return _context.Articles.Include(a => a.ApplicationUser).Include(a => a.ArticleRatings).Include(a => a.ArticleComments).SingleOrDefaultAsync(a => a.ArticleId == id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles.Include(a => a.ApplicationUser).Include(a => a.ArticleRatings).Include(a => a.ArticleComments).ToList();
        }

        public Task<List<Article>> GetAllAsync()
        {
            return _context.Articles.Include(a => a.ApplicationUser).Include(a => a.ArticleRatings).Include(a => a.ArticleComments).ToListAsync();
        }
    }
}
