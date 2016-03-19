using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class ArticleRatingRepository : IArticleRatingRepository
    {
        private WebForDbContext _context;

        public ArticleRatingRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleRating entity)
        {
            throw new NotImplementedException();
        }

        public ArticleRating FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleRating> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleRating> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleRating>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

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

        public double CalculateArticleRating(int id)
        {
            var articleRatings = _context.ArticleRatings.Where(a => a.ArticleIDfk == id).ToList();

            return articleRatings.Sum(a => a.ArticleRatingScore)/articleRatings.Count;
        }

        public Task<ArticleRating> GetCurrentUserRating(int articleId, string userIDfk)
        {
            return _context.ArticleRatings.SingleOrDefaultAsync(a => a.ArticleIDfk == articleId && a.UserIDfk == userIDfk);
        }

        public Task<int> UpdateArticleRating(int id, double rating, string userIDfk)
        {
            var articleRating = _context.ArticleRatings.Single(a => a.ArticleIDfk == id && a.UserIDfk == userIDfk);

            articleRating.ArticleRatingScore = rating;

            return _context.SaveChangesAsync();
        }

        public Task<int> GetNumberOfVoters(int articleId)
        {
            return _context.ArticleRatings.Where(a => a.ArticleIDfk == articleId).CountAsync();
        }
    }
}
