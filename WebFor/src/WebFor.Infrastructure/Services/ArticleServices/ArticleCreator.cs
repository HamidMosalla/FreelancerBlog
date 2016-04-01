using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Enums;
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

        public async Task<List<ArticleStatus>> CreateNewArticleAsync(Article article, string articleTags)
        {
            var articleStatusList = new List<ArticleStatus>();
            int addTagsResult = 0;
            int addTagsToArticleResult = 0;

            article.ArticleDateCreated = DateTime.Now;
            article.ArticleViewCount = 1;

            article.UserIDfk = contextAccessor.HttpContext.User.GetUserId();

            _uw.ArticleRepository.Add(article);

            int addArticleResult = await _uw.SaveAsync();

            if (!string.IsNullOrEmpty(articleTags))
            {
                var viewModelTags = articleTags.Split(',');

                addTagsResult = await _uw.ArticleTagRepository.AddRangeOfTags(viewModelTags);

                var tagsToAddToArticle = await _uw.ArticleTagRepository.FindByTagsName(viewModelTags);

                addTagsToArticleResult = await _uw.ArticleTagRepository.AddTagRangeToArticle(tagsToAddToArticle, article);

                #region Throw away code after refactoring tags related operations
                //var preExistringTags = await _uw.ArticleTagRepository.GetAllAsync();
                //var newTags = articleTags.Split(',');

                //var tagList = new List<ArticleTag>();
                //var joinTableArtTagList = new List<ArticleArticleTag>();
                //ArticleTag tag;

                //foreach (var item in newTags)
                //{
                //    if (preExistringTags.All(p => p.ArticleTagName != item))
                //    {
                //        tag = new ArticleTag { ArticleTagName = item };
                //        tagList.Add(tag);


                //    }
                //    else
                //    {
                //        tag = preExistringTags.Single(p => p.ArticleTagName == item);
                //    }

                //    var joinTableArticleTag = new ArticleArticleTag
                //    {
                //        Article = article,
                //        ArticleTag = tag
                //    };
                //    joinTableArtTagList.Add(joinTableArticleTag);
                //}

                ////await _uw.ArticleTagRepository.AddRangeOfTags(tagList); just for now, it broke the create method

                //_uw.ArticleArticleTagRepository.AddRange(joinTableArtTagList);
                #endregion

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

    }
}
