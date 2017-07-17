using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class EditArticleCommandHandler : IAsyncRequestHandler<EditArticleCommand, List<ArticleStatus>>
    {
        private FreelancerBlogContext _context;
        private IMediator _mediator;

        public EditArticleCommandHandler(FreelancerBlogContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<ArticleStatus>> Handle(EditArticleCommand message)
        {
            message.Article.ArticleDateModified = DateTime.Now;
            var articleStatusList = new List<ArticleStatus>();
            int addTagsResult = 0;
            int removeTagsFromArticleResult = 0;
            int addTagsToArticleResult = 0;
            int tagsRemovalResult = 0;

            int updateArticleResult = await UpdateArticleAsync(message.Article);

            var currentArticleTags = await GetCurrentArticleTagsAsync(message.Article.ArticleId);

            if (!string.IsNullOrEmpty(message.ArticleTags))
            {
                var viewModelTags = message.ArticleTags.Split(',');

                var exceptRemovedTags = currentArticleTags.Select(c => c.ArticleTagName).Except(viewModelTags);

                var exceptAddedTags = viewModelTags.Except(currentArticleTags.Select(c => c.ArticleTagName));

                var tagsToRemoveFromArticle = await FindByTagsName(exceptRemovedTags);

                addTagsResult = await AddRangeOfTags(exceptAddedTags);

                var tagsToAddToArticle = await FindByTagsName(exceptAddedTags);

                removeTagsFromArticleResult = await RemoveTagRangeFromArticle(tagsToRemoveFromArticle, message.Article.ArticleId);

                addTagsToArticleResult = await AddTagRangeToArticle(tagsToAddToArticle, message.Article);
            }
            else
            {
                tagsRemovalResult = await RemoveTagRangeFromArticle(currentArticleTags, message.Article.ArticleId);
            }

            if (updateArticleResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleEditSucess);
            }

            if (addTagsResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleTagCreateSucess);
            }

            if (removeTagsFromArticleResult > 0 || tagsRemovalResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleRemoveTagsFromArticleSucess);
            }

            if (addTagsToArticleResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleArticleTagsCreateSucess);
            }

            return articleStatusList;
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
            return
                _context.ArticleArticleTags.Where(a => a.ArticleId == articleId)
                    .Join(_context.ArticleTags.ToList(), aat => aat.ArticleTagId, at => at.ArticleTagId, (aat, at) => at)
                    .ToListAsync();
        }

        public Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> delimitedTags)
        {
            return _context.ArticleTags.Join(delimitedTags, a => a.ArticleTagName, e => e, (a, e) => a).ToListAsync();
        }

        public Task<int> AddRangeOfTags(IEnumerable<string> exceptAddedTags)
        {
            var listOfAllTags = _context.ArticleTags.ToList();

            foreach (var item in exceptAddedTags)
            {
                if (listOfAllTags.All(a => a.ArticleTagName != item))
                {
                    _context.ArticleTags.Add(new ArticleTag { ArticleTagName = item });
                }
            }

            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveTagRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId)
        {
            var listOfaATags =
                _context.ArticleArticleTags.Join(tagsToRemove, a => a.ArticleTagId, t => t.ArticleTagId, (a, t) => a)
                                           .Where(c => c.ArticleId == articleId)
                                           .ToList();

            _context.ArticleArticleTags.RemoveRange(listOfaATags);

            return _context.SaveChangesAsync();
        }

        public Task<int> AddTagRangeToArticle(List<ArticleTag> tagsToAdd, Article article)
        {
            foreach (var item in tagsToAdd)
            {
                _context.ArticleArticleTags.Add(new ArticleArticleTag { Article = article, ArticleTag = item });
            }

            return _context.SaveChangesAsync();
        }
    }
}