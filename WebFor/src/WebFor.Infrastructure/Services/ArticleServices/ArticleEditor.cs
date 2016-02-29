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

        public ArticleEditor(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public async Task<int> EditArticleAsync(Article article, string articleTags)
        {
            article.ArticleDateModified = DateTime.Now;

            #region Refactored Article Update
            //current approach is probably better, one less query to the database
            //var oldArticle = await _uw.ArticleRepository.FindByIdAsync(article.ArticleId);

            //oldArticle.ArticleBody = article.ArticleBody;
            //oldArticle.ArticleTitle = article.ArticleTitle;
            //oldArticle.ArticleSummary = article.ArticleSummary;
            //oldArticle.ArticleStatus = article.ArticleStatus;
            //oldArticle.ArticleDateModified = DateTime.Now;
            #endregion

            int updateArticleResult = await _uw.ArticleRepository.UpdateArticleAsync(article);
            //TODO: need to get decomposed, don't forget your solid heritage
            
            var currentArticleTags = await _uw.ArticleRepository.GetCurrentArticleTagsAsync(article.ArticleId);

            if (!string.IsNullOrEmpty(articleTags))
            {
                var viewModelTags = articleTags.Split(',');

                var exept = currentArticleTags.Select(c => c.ArticleTagName).Except(viewModelTags);

                var tagsToRemoveFromArticle = await _uw.ArticleTagRepository.FindByTagsName(exept);

                int tagsRemovalResult = await _uw.ArticleTagRepository.RemoveRangeFromArticle(tagsToRemoveFromArticle, article.ArticleId);

                var preExistingTags = await _uw.ArticleTagRepository.GetAllAsync();

                var tagList = new List<ArticleTag>();
                var joinTableArtTagList = new List<ArticleArticleTag>();
                ArticleTag tag;

                foreach (var item in viewModelTags)
                {
                    if (preExistingTags.All(p => p.ArticleTagName != item))
                    {
                        tag = new ArticleTag { ArticleTagName = item };
                        tagList.Add(tag);
                    }
                    else
                    {
                        tag = preExistingTags.Single(p => p.ArticleTagName == item);
                    }

                    if (currentArticleTags.All(c => c.ArticleTagId != tag.ArticleTagId))
                    {
                        var joinTableArticleTag = new ArticleArticleTag
                        {
                            Article = article,
                            ArticleTag = tag
                        };
                        joinTableArtTagList.Add(joinTableArticleTag);
                    }

                }

                _uw.ArticleTagRepository.AddRange(tagList);
                await _uw.SaveAsync();

                _uw.ArticleArticleTagRepository.AddRange(joinTableArtTagList);
                await _uw.SaveAsync();
            }
            else
            {
                int tagsRemovalResult = await _uw.ArticleTagRepository.RemoveRangeFromArticle(currentArticleTags, article.ArticleId);
            }

            return updateArticleResult;
        }
    }
}
