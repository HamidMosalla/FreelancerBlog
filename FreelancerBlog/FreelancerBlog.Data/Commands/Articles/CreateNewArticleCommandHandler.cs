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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Articles
{
    class CreateNewArticleCommandHandler : IAsyncRequestHandler<CreateNewArticleCommand, List<ArticleStatus>>
    {
        private FreelancerBlogContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateNewArticleCommandHandler(FreelancerBlogContext context, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<List<ArticleStatus>> Handle(CreateNewArticleCommand message)
        {
            var articleStatusList = new List<ArticleStatus>();
            int addTagsResult = 0;
            int addTagsToArticleResult = 0;

            message.Article.ArticleDateCreated = DateTime.Now;
            message.Article.ArticleViewCount = 1;

            message.Article.UserIDfk = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            _context.Articles.Add(message.Article);

            int addArticleResult = await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(message.ArticleTags))
            {
                var viewModelTags = message.ArticleTags.Split(',');

                addTagsResult = await AddRangeOfTags(viewModelTags);

                var tagsToAddToArticle = await _context.ArticleTags.Join(viewModelTags, a => a.ArticleTagName, e => e, (a, e) => a).ToListAsync();

                addTagsToArticleResult = await AddTagRangeToArticle(tagsToAddToArticle, message.Article);

            }

            if (addArticleResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleCreateSucess);
            }

            if (addTagsResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleTagCreateSucess);
            }

            if (addTagsToArticleResult > 0)
            {
                articleStatusList.Add(ArticleStatus.ArticleArticleTagsCreateSucess);
            }

            return articleStatusList;
        }

        private Task<int> AddRangeOfTags(IEnumerable<string> exceptAddedTags)
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

        private Task<int> AddTagRangeToArticle(List<ArticleTag> tagsToAdd, Article article)
        {
            foreach (var item in tagsToAdd)
            {
                _context.ArticleArticleTags.Add(new ArticleArticleTag { Article = article, ArticleTag = item });
            }

            return _context.SaveChangesAsync();
        }
    }
}