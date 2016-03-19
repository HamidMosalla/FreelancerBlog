using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Http;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;
using WebFor.Web.Areas.Admin.ViewModels.Article;
using System.Security.Claims;

namespace WebFor.Web.Services
{
    public interface IWebForMapper
    {
        ArticleViewModel ArticleToArticleViewModel(Article article);
        Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article);
        Article ArticleViewModelToArticle(ArticleViewModel articleViewModel);
        List<ArticleViewModel> ArticleCollectionToArticleViewModelCollection(List<Article> articles);
    }

    public class WebForMapper : IWebForMapper
    {

        private IUnitOfWork _uw;
        private readonly IHttpContextAccessor contextAccessor;

        public WebForMapper(IUnitOfWork uw, IHttpContextAccessor contextAccessor)
        {
            _uw = uw;
            this.contextAccessor = contextAccessor;
        }

        static readonly MapperConfiguration _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Article, ArticleViewModel>();
            cfg.CreateMap<ArticleViewModel, Article>();
        });

        public IMapper Mapper = _autoMapperConfig.CreateMapper();

        public ArticleViewModel ArticleToArticleViewModel(Article article)
        {
            var articleViewModel = Mapper.Map<Article, ArticleViewModel>(article);

            return articleViewModel;
        }

        public async Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article)
        {
            var articleViewModel = Mapper.Map<Article, ArticleViewModel>(article);

            articleViewModel.ArticleTags = await _uw.ArticleTagRepository.GetTagsByArticleIdAsync(article.ArticleId);
            articleViewModel.ArticleTagsList = await _uw.ArticleRepository.GetCurrentArticleTagsAsync(article.ArticleId);
            articleViewModel.SumOfRating = _uw.ArticleRatingRepository.CalculateArticleRating(article.ArticleId);
            articleViewModel.CurrentUserRating = await _uw.ArticleRatingRepository.GetCurrentUserRating(article.ArticleId, contextAccessor.HttpContext.User.GetUserId());
            articleViewModel.NumberOfVoters = await _uw.ArticleRatingRepository.GetNumberOfVoters(article.ArticleId);

            return articleViewModel;
        }

        public List<ArticleViewModel> ArticleCollectionToArticleViewModelCollection(List<Article> articles)
        {
            return Mapper.Map<List<Article>, List<ArticleViewModel>>(articles);
        }

        public Article ArticleViewModelToArticle(ArticleViewModel articleViewModel)
        {
            return Mapper.Map<ArticleViewModel, Article>(articleViewModel);
        }
    }
}