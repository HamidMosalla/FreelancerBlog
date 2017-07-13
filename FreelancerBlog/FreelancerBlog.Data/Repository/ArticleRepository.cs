using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private FreelancerBlogContext _context;

        public ArticleRepository(FreelancerBlogContext context)
        {
            _context = context;
        }

        #region Article Aggregate Root
        public void Add(Article entity)
        {
            _context.Articles.Add(entity);
        }

        public void Remove(Article entity)
        {
            _context.Articles.Remove(entity);
        }

        public void Update(Article entity)
        {
            _context.Articles.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<Article> FindByIdAsync(int id)
        {
            return
                _context.Articles.Include(a => a.ApplicationUser)
                    .Include(a => a.ArticleRatings)
                    .Include(a => a.ArticleComments)
                    .SingleOrDefaultAsync(a => a.ArticleId == id);
        }

        public Task<List<Article>> GetAllAsync()
        {
            return
                _context.Articles.Include(a => a.ApplicationUser)
                    .Include(a => a.ArticleRatings)
                    .Include(a => a.ArticleComments)
                    .ToListAsync();
        }



        public Task<int> DeleteArticleAsync(Article article)
        {
            _context.Articles.Remove(article);
            return _context.SaveChangesAsync();
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

        public async Task<int> IncreaseArticleViewCount(int articleId)
        {
            var article = await _context.Articles.SingleAsync(a => a.ArticleId == articleId);

            article.ArticleViewCount += 1;

            return await _context.SaveChangesAsync();
        }

        public Task<List<Article>> GetArticlesByTag(int tagId)
        {
            return
                _context.ArticleArticleTags.Where(a => a.ArticleTagId.Equals(tagId))
                    .Join(
                        _context.Articles.Include(a => a.ApplicationUser)
                            .Include(a => a.ArticleComments)
                            .Include(a => a.ArticleRatings), left => left.ArticleId, right => right.ArticleId,
                        (aat, a) => a)
                    .ToListAsync();
        }

        public Task<List<Article>> GetLatestArticles(int numberOfArticles)
        {
            return _context.Articles.OrderByDescending(a => a.ArticleDateCreated).Take(numberOfArticles).ToListAsync();
        }
        #endregion

        #region ArticleArticleTag
        public void AddArticleArticleTag(ArticleArticleTag entity)
        {
            _context.ArticleArticleTags.Add(entity);
        }

        public Task<List<ArticleArticleTag>> GetAllArticleArticleTagAsync()
        {
            return _context.ArticleArticleTags.ToListAsync();
        }

        public void AddRangeOfArticleArticleTag(List<ArticleArticleTag> joinTableArtTagList)
        {
            _context.ArticleArticleTags.AddRange(joinTableArtTagList);
        }
        #endregion

        #region ArticleTag
        public void AddArticleTag(ArticleTag entity)
        {
            _context.ArticleTags.Add(entity);
        }

        public void RemoveArticleTag(ArticleTag entity)
        {
            _context.ArticleTags.Remove(entity);
        }

        public void UpdateArticleTag(ArticleTag entity)
        {
            _context.ArticleTags.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<ArticleTag> FindArticleTagByIdAsync(int id)
        {
            return _context.ArticleTags.SingleOrDefaultAsync(a => a.ArticleTagId.Equals(id));
        }

        public IEnumerable<ArticleTag> GetAllArticleTags()
        {
            return _context.ArticleTags.ToList();
        }

        public Task<List<ArticleTag>> GetAllArticleTagsAsync()
        {
            return _context.ArticleTags.ToListAsync();
        }

        public Task<string[]> GetAllTagNamesArrayAsync()
        {
            return _context.ArticleTags.Select(a => a.ArticleTagName).ToArrayAsync();
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

        public async Task<string> GetTagsByArticleIdAsync(int articleId)
        {
            var listOfArticleTags = await _context.ArticleArticleTags.Where(a => a.ArticleId == articleId).ToListAsync();

            var arrayOfTags =
                listOfArticleTags.Join(
                    await _context.ArticleTags.ToListAsync(), a => a.ArticleTagId, t => t.ArticleTagId,
                    (a, t) => t.ArticleTagName).ToArray();

            return string.Join(",", arrayOfTags);
        }

        public Task<List<ArticleTag>> FindByTagsName(IEnumerable<string> delimitedTags)
        {
            return _context.ArticleTags.Join(delimitedTags, a => a.ArticleTagName, e => e, (a, e) => a).ToListAsync();
        }

        public Task<int> RemoveRangeOfArticleTags(List<ArticleTag> tagsToRemove)
        {
            _context.ArticleTags.RemoveRange(tagsToRemove);
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
                _context.ArticleArticleTags.Add(new ArticleArticleTag {Article = article, ArticleTag = item});
            }

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteArticleTagAsync(ArticleTag model)
        {
            _context.ArticleTags.Remove(model);

            return _context.SaveChangesAsync();
        }

        public Task<int> EditArticleTagAsync(ArticleTag model, string newTagName)
        {
            model.ArticleTagName = newTagName;
            return _context.SaveChangesAsync();
        }
        #endregion

        #region ArticleRatingRepository
        public Task<int> AddRatingForArticle(int id, double rating, string userIDfk)
        {
            var articleRating = new ArticleRating { ArticleIDfk = id, ArticleRatingScore = rating, UserIDfk = userIDfk };

            _context.ArticleRatings.Add(articleRating);

            return _context.SaveChangesAsync();
        }

        public bool IsRatedBefore(int id, string userIDfk)
        {
            var ratings = _context.ArticleRatings.Where(a => a.ArticleIDfk == id).ToList();

            if (ratings.Count != 0)
            {
                bool eligibility = ratings.Any(r => r.UserIDfk == userIDfk);

                if (eligibility)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public Task<int> UpdateArticleRating(int id, double rating, string userIDfk)
        {
            var articleRating = _context.ArticleRatings.Single(a => a.ArticleIDfk == id && a.UserIDfk == userIDfk);

            articleRating.ArticleRatingScore = rating;

            return _context.SaveChangesAsync();
        }
        #endregion

        #region ArticleComment
        public Task<ArticleComment> FindCommentByIdAsync(int id)
        {
            return _context.ArticleComments.SingleOrDefaultAsync(a => a.ArticleCommentId.Equals(id));
        }

        public Task<List<ArticleComment>> GetAllCommentAsync()
        {
            return
                _context.ArticleComments.OrderBy(a => a.IsCommentApproved)
                    .ThenByDescending(a => a.ArticleCommentDateCreated)
                    .Include(a => a.Article)
                    .Include(a => a.ApplicationUser)
                    .ToListAsync();
        }

        public Task<int> AddCommentToArticle(ArticleComment articleComment)
        {
            _context.ArticleComments.Add(articleComment);

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteArticleCommentAsync(ArticleComment model)
        {
            _context.ArticleComments.Remove(model);
            return _context.SaveChangesAsync();
        }

        public Task<int> ToggleArticleCommentApproval(ArticleComment model)
        {
            model.IsCommentApproved = !model.IsCommentApproved;

            return _context.SaveChangesAsync();
        }

        public Task<int> EditArticleCommentAsync(ArticleComment model, string newCommentBody)
        {
            model.ArticleCommentBody = newCommentBody;
            return _context.SaveChangesAsync();
        }
        #endregion
    }
}
