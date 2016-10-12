using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleRepository : IRepository<Article, int>
    {
        #region Article Aggregate Root
        Task<int> UpdateArticleAsync(Article article);

        Task<int> DeleteArticleAsync(Article article);
        Task<List<ArticleTag>> GetCurrentArticleTagsAsync(int articleId);

        Task<int> IncreaseArticleViewCount(int articleId);

        Task<List<Article>> GetArticlesByTag(int tagId);
        Task<List<Article>> GetLatestArticles(int numberOfArticles);
        #endregion

        #region ArticleArticleTag
        void AddArticleArticleTag(ArticleArticleTag entity);

        Task<List<ArticleArticleTag>> GetAllArticleArticleTagAsync();

        void AddRangeOfArticleArticleTag(List<ArticleArticleTag> joinTableArtTagList);
        #endregion

        #region ArticleTag

        void AddArticleTag(ArticleTag entity);

        void RemoveArticleTag(ArticleTag entity);

        void UpdateArticleTag(ArticleTag entity);

        Task<ArticleTag> FindArticleTagByIdAsync(int id);

        IEnumerable<ArticleTag> GetAllArticleTags();

        Task<List<ArticleTag>> GetAllArticleTagsAsync();
        Task<string[]> GetAllTagNamesArrayAsync();

        Task<int> AddRangeOfTags(IEnumerable<string> exceptAddedTags);

        Task<string> GetTagsByArticleIdAsync(int articleId);

        Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> delimitedTags);

        Task<int> RemoveRangeOfArticleTags(List<ArticleTag> tagsToRemove);

        Task<int> RemoveTagRangeFromArticle(List<ArticleTag> tagsToRemove, int articleId);

        Task<int> AddTagRangeToArticle(List<ArticleTag> tagsToAdd, Article article);
        Task<int> DeleteArticleTagAsync(ArticleTag model);
        Task<int> EditArticleTagAsync(ArticleTag model, string newTagName);
        #endregion

        #region ArticleRating
        Task<int> AddRatingForArticle(int id, double rating, string userIDfk);

        bool IsRatedBefore(int id, string userIDfk);

        Task<int> UpdateArticleRating(int id, double rating, string userIDfk);
        #endregion

        #region ArticleComment
        Task<ArticleComment> FindCommentByIdAsync(int id);
        Task<List<ArticleComment>> GetAllCommentAsync();
        Task<int> AddCommentToArticle(ArticleComment articleComment);
        Task<int> DeleteArticleCommentAsync(ArticleComment model);
        Task<int> ToggleArticleCommentApproval(ArticleComment model);
        Task<int> EditArticleCommentAsync(ArticleComment model, string newCommentBody);
        #endregion
    }
}
