using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.ArticleTags;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    class UpdateArticleTagsCommandHandler : IAsyncRequestHandler<UpdateArticleTagsCommand>
    {
        private readonly FreelancerBlogContext _context;

        public UpdateArticleTagsCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateArticleTagsCommand message)
        {
            var articleTags = GetCurrentArticle(message.Article.ArticleId);
           var currentArticleTags = articleTags.Select(t=> t.ArticleTagName.Trim()).ToList();

            if (string.IsNullOrEmpty(message.ArticleTags))
            {
                await RemoveTagRangeFromArticle(articleTags, message.Article.ArticleId);
            }

            var viewModelTags = message.ArticleTags.Split(',').Select(t => t.Trim()).ToList();

            var tagsToRemove = currentArticleTags.Except(viewModelTags);

            var tagsToAdd = viewModelTags.Except(currentArticleTags).ToList();

            var tagsToRemoveFromArticle = await FindByTagsName(tagsToRemove);

            await AddRangeOfTags(tagsToAdd);

            var tagsToAddToArticle = await FindByTagsName(tagsToAdd);

            await RemoveTagRangeFromArticle(tagsToRemoveFromArticle, message.Article.ArticleId);

            await AddTagRangeToArticle(tagsToAddToArticle, message.Article);
        }

        private Task<int> RemoveTagRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId)
        {
            var listOfaATags = _context.ArticleArticleTags
                                       .Join(tagsToRemove, a => a.ArticleTagId, t => t.ArticleTagId, (a, t) => a)
                                       .Where(c => c.ArticleId == articleId)
                                       .ToList();

            _context.ArticleArticleTags.RemoveRange(listOfaATags);

            return _context.SaveChangesAsync();
        }
        private List<ArticleTag> GetCurrentArticle(int articleId)
        {
            return _context.ArticleArticleTags
                           .Where(a => a.ArticleId == articleId)
                           .Join(_context.ArticleTags.ToList(), aat => aat.ArticleTagId, at => at.ArticleTagId, (aat, at) => at)
                           .ToList();
        }
        private Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> delimitedTags)
        {
            return _context.ArticleTags.Join(delimitedTags, a => a.ArticleTagName, e => e, (a, e) => a).ToListAsync();
        }
        private Task<int> AddRangeOfTags(IEnumerable<string> tagsToAdd)
        {
            var articleTags = tagsToAdd.Select(t => new ArticleTag {ArticleTagName = t});
            _context.ArticleTags.AddRange(articleTags);
            return _context.SaveChangesAsync();
        }
        private Task<int> AddTagRangeToArticle(List<ArticleTag> tagsToAdd, Article article)
        {
            var articleArticleTag = tagsToAdd.Select(t => new ArticleArticleTag {Article = article, ArticleTag = t});
            _context.ArticleArticleTags.AddRange(articleArticleTag);
            return _context.SaveChangesAsync();
        }
    }
}