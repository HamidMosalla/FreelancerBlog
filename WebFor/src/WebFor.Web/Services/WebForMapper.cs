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
using WebFor.Web.ViewModels.Contact;
using WebFor.Web.Areas.Admin.ViewModels.SlideShow;
using WebFor.Web.Areas.Admin.ViewModels.Portfolio;

namespace WebFor.Web.Services
{
    public interface IWebForMapper
    {
        ArticleViewModel ArticleToArticleViewModel(Article article);
        Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article);
        Article ArticleViewModelToArticle(ArticleViewModel articleViewModel);
        List<ArticleViewModel> ArticleCollectionToArticleViewModelCollection(List<Article> articles);
        List<SlideShowViewModel> SlideShowCollectionToSlideShowCollectionViewModel(List<SlideShow> slideShows);
        ArticleComment ArticleCommentViewModelToArticleComment(ArticleCommentViewModel viewModel);
        List<ArticleCommentViewModel> ArticleCommentCollectionToArticleCommentViewModelCollection(List<ArticleComment> comments);
        List<ArticleTagViewModel> ArticleTagCollectionToArticleTagViewModelCollection(List<ArticleTag> tags);
        List<ContactViewModel> ContactCollectionToContactViewModelCollection(List<Contact> contacts);
        SlideShow SlideShowViewModelToSlideShow(SlideShowViewModel slideShowViewModel, string imagePath);
        Contact ContactViewModelToContact(ContactViewModel contactViewModel);
        List<PortfolioViewModel> PortfolioCollectionToPortfolioViewModelCollection(List<Portfolio> portfolios);
        SlideShowViewModelEdit SlideShowToSlideShowViewModelEdit(SlideShow model);
        Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName);
        SlideShow SlideShowViewModelEditToSlideShow(SlideShowViewModelEdit viewModel);
        PortfolioViewModelEdit PortfolioToPortfolioViewModelEdit(Portfolio model);
        Portfolio PortfolioViewModelEditToPortfolio(PortfolioViewModelEdit viewModel);
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
            cfg.CreateMap<ArticleTag, ArticleTagViewModel>();
            cfg.CreateMap<ArticleTagViewModel, ArticleTag>();
            cfg.CreateMap<Contact, ContactViewModel>();
            cfg.CreateMap<ContactViewModel, Contact>();
            cfg.CreateMap<SlideShow, SlideShowViewModel>();
            cfg.CreateMap<SlideShowViewModel, SlideShow>();
            cfg.CreateMap<SlideShow, SlideShowViewModelEdit>();
            cfg.CreateMap<SlideShowViewModelEdit, SlideShow>();
            cfg.CreateMap<Portfolio, PortfolioViewModel>();
            cfg.CreateMap<PortfolioViewModel, Portfolio>();
            cfg.CreateMap<Portfolio, PortfolioViewModelEdit>();
            cfg.CreateMap<PortfolioViewModelEdit, Portfolio>();
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

        public List<SlideShowViewModel> SlideShowCollectionToSlideShowCollectionViewModel(List<SlideShow> slideShows)
        {
            return Mapper.Map<List<SlideShow>, List<SlideShowViewModel>>(slideShows);
        }

        public List<ArticleCommentViewModel> ArticleCommentCollectionToArticleCommentViewModelCollection(List<ArticleComment> comments)
        {
            return Mapper.Map<List<ArticleComment>, List<ArticleCommentViewModel>>(comments);
        }

        public List<ArticleTagViewModel> ArticleTagCollectionToArticleTagViewModelCollection(List<ArticleTag> tags)
        {
            return Mapper.Map<List<ArticleTag>, List<ArticleTagViewModel>>(tags);
        }

        public List<ContactViewModel> ContactCollectionToContactViewModelCollection(List<Contact> contacts)
        {
            return Mapper.Map<List<Contact>, List<ContactViewModel>>(contacts);
        }

        public SlideShow SlideShowViewModelToSlideShow(SlideShowViewModel slideShowViewModel, string imagePath)
        {
            var slideShow = Mapper.Map<SlideShowViewModel, SlideShow>(slideShowViewModel);

            slideShow.SlideShowPictrure = imagePath;

            return slideShow;
        }

        public Contact ContactViewModelToContact(ContactViewModel contactViewModel)
        {
            return Mapper.Map<ContactViewModel, Contact>(contactViewModel);
        }

        public List<PortfolioViewModel> PortfolioCollectionToPortfolioViewModelCollection(List<Portfolio> portfolios)
        {
            return Mapper.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios);
        }

        public SlideShowViewModelEdit SlideShowToSlideShowViewModelEdit(SlideShow model)
        {
            return Mapper.Map<SlideShow, SlideShowViewModelEdit>(model);
        }

        public Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName)
        {
            var portfolio =  Mapper.Map<PortfolioViewModel, Portfolio>(viewModel);

            portfolio.PortfolioThumbnail = thumbFileName;

            return portfolio;
        }

        public SlideShow SlideShowViewModelEditToSlideShow(SlideShowViewModelEdit viewModel)
        {
            return Mapper.Map< SlideShowViewModelEdit, SlideShow>(viewModel);
        }

        public PortfolioViewModelEdit PortfolioToPortfolioViewModelEdit(Portfolio model)
        {
            var portfolioViewModelEdit =  Mapper.Map<Portfolio, PortfolioViewModelEdit>(model);

            portfolioViewModelEdit.CurrentThumbnail = model.PortfolioThumbnail;

            return portfolioViewModelEdit;
        }

        public Portfolio PortfolioViewModelEditToPortfolio(PortfolioViewModelEdit viewModel)
        {
            return Mapper.Map<PortfolioViewModelEdit, Portfolio>(viewModel);
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