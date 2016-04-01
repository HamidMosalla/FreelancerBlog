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
    public class ArticleEditor : IArticleEditor
    {
        private IUnitOfWork _uw;
        public ArticleStatus ArticleStatus { get; }

        public ArticleEditor(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public async Task<List<ArticleStatus>> EditArticleAsync(Article article, string articleTags)
        {
            article.ArticleDateModified = DateTime.Now;
            var articleStatusList = new List<ArticleStatus>();
            int addTagsResult = 0;
            int removeTagsFromArticleResult = 0;
            int addTagsToArticleResult = 0;
            int tagsRemovalResult = 0;

            #region Leftover code related to article update
            //current approach is probably better, one less query to the database
            //var oldArticle = await _uw.ArticleRepository.FindByIdAsync(article.ArticleId);

            //oldArticle.ArticleBody = article.ArticleBody;
            //oldArticle.ArticleTitle = article.ArticleTitle;
            //oldArticle.ArticleSummary = article.ArticleSummary;
            //oldArticle.ArticleStatus = article.ArticleStatus;
            //oldArticle.ArticleDateModified = DateTime.Now;
            #endregion

            int updateArticleResult = await _uw.ArticleRepository.UpdateArticleAsync(article);

            var currentArticleTags = await _uw.ArticleRepository.GetCurrentArticleTagsAsync(article.ArticleId);

            if (!string.IsNullOrEmpty(articleTags))
            {
                var viewModelTags = articleTags.Split(',');

                var exceptRemovedTags = currentArticleTags.Select(c => c.ArticleTagName).Except(viewModelTags);

                var exceptAddedTags = viewModelTags.Except(currentArticleTags.Select(c => c.ArticleTagName));

                var tagsToRemoveFromArticle = await _uw.ArticleTagRepository.FindByTagsName(exceptRemovedTags);

                addTagsResult = await _uw.ArticleTagRepository.AddRangeOfTags(exceptAddedTags);

                var tagsToAddToArticle = await _uw.ArticleTagRepository.FindByTagsName(exceptAddedTags);

                removeTagsFromArticleResult = await _uw.ArticleTagRepository.RemoveTagRangeFromArticle(tagsToRemoveFromArticle, article.ArticleId);

                addTagsToArticleResult = await _uw.ArticleTagRepository.AddTagRangeToArticle(tagsToAddToArticle, article);

                #region Throw away code after refactoring tags related operations
                //var preExistingTags = await _uw.ArticleTagRepository.GetAllAsync();

                //var tagList = new List<ArticleTag>();
                //var joinTableArtTagList = new List<ArticleArticleTag>();
                //ArticleTag tag;

                //foreach (var item in viewModelTags)
                //{
                //    if (preExistingTags.All(p => p.ArticleTagName != item))
                //    {
                //        tag = new ArticleTag { ArticleTagName = item };
                //        tagList.Add(tag);
                //    }
                //    else
                //    {
                //        tag = preExistingTags.Single(p => p.ArticleTagName == item);
                //    }

                //    if (currentArticleTags.All(c => c.ArticleTagId != tag.ArticleTagId))
                //    {
                //        var joinTableArticleTag = new ArticleArticleTag
                //        {
                //            Article = article,
                //            ArticleTag = tag
                //        };
                //        joinTableArtTagList.Add(joinTableArticleTag);
                //    }

                //}

                //_uw.ArticleTagRepository.AddRange(tagList);
                //await _uw.SaveAsync();

                //_uw.ArticleArticleTagRepository.AddRange(joinTableArtTagList);
                //await _uw.SaveAsync();
                #endregion

            }
            else
            {
                tagsRemovalResult = await _uw.ArticleTagRepository.RemoveTagRangeFromArticle(currentArticleTags, article.ArticleId);
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

    }
}
