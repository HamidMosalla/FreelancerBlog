using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.ArticleServices;

namespace WebFor.Infrastructure.Services.ArticleServices
{
    public class ArticleCreator : IArticleCreator
    {
        private IUnitOfWork _uw;
        private readonly IHttpContextAccessor contextAccessor;

        public ArticleCreator(IUnitOfWork uw, IHttpContextAccessor contextAccessor)
        {
            _uw = uw;
            this.contextAccessor = contextAccessor;
        }
        public async Task<int> CreateNewArticleAsync(Article article, string articleTags)
        {
            article.ArticleDateCreated = DateTime.Now;
            article.UserIDfk = contextAccessor.HttpContext.User.GetUserId();

            _uw.ArticleRepository.Add(article);
            //TODO: need to get decomposed, don't forget your solid heritage

            var preExistringTags = await _uw.ArticleTagRepository.GetAllAsync();
            var newTags = articleTags.Split(',');

            var tagList = new List<ArticleTag>();
            var joinTableArtTagList = new List<ArticleArticleTag>();
            ArticleTag tag;

            foreach (var item in newTags)
            {
                if (preExistringTags.All(p => p.ArticleTagName != item))
                {
                    tag = new ArticleTag { ArticleTagName = item };
                    tagList.Add(tag);

                    
                }
                else
                {
                    tag = preExistringTags.Single(p => p.ArticleTagName == item);
                }

                var joinTableArticleTag = new ArticleArticleTag
                {
                    Article = article,
                    ArticleTag = tag
                };
                joinTableArtTagList.Add(joinTableArticleTag);
            }

            _uw.ArticleTagRepository.AddRange(tagList);
            await _uw.SaveAsync();

            _uw.ArticleArticleTagRepository.AddRange(joinTableArtTagList);
            return await _uw.SaveAsync();
        }
    }
}
