using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Http;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;
using WebFor.Web.Areas.Admin.ViewModels.Article;
using System.Security.Claims;
using System.Linq;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.Services
{
    public interface IWebForMapper
    {
        ArticleViewModel ArticleToArticleViewModel(Article article);
        Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article);
        Article ArticleViewModelToArticle(ArticleViewModel articleViewModel);
        List<ArticleViewModel> ArticleCollectionToArticleViewModelCollection(List<Article> articles);
        ArticleComment ArticleCommentViewModelToArticleComment(ArticleCommentViewModel viewModel);
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
            cfg.CreateMap<ArticleCommentViewModel, ArticleComment>();
            cfg.CreateMap<ArticleComment, ArticleCommentViewModel>();
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


            articleViewModel.SumOfRating = articleViewModel.ArticleRatings.Sum(a => a.ArticleRatingScore) / articleViewModel.ArticleRatings.Count;
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == contextAccessor.HttpContext.User.GetUserId());

            return articleViewModel;
        }

        public List<ArticleViewModel> ArticleCollectionToArticleViewModelCollection(List<Article> articles)
        {
            return Mapper.Map<List<Article>, List<ArticleViewModel>>(articles);
        }

        public ArticleComment ArticleCommentViewModelToArticleComment(ArticleCommentViewModel viewModel)
        {
            return Mapper.Map<ArticleCommentViewModel, ArticleComment > (viewModel);
        }

        public Article ArticleViewModelToArticle(ArticleViewModel articleViewModel)
        {
            return Mapper.Map<ArticleViewModel, Article>(articleViewModel);
        }
    }
}