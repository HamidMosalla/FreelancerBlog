using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebFor.Web.Areas.Admin.ViewModels.Article;
using WebFor.Web.Areas.Admin.ViewModels.Portfolio;
using WebFor.Web.Areas.Admin.ViewModels.SlideShow;
using WebFor.Web.Areas.User.ViewModels.Profile;
using WebFor.Web.ViewModels.Article;
using WebFor.Web.ViewModels.Contact;
using WebFor.Web.ViewModels.SiteOrder;
using WebFor.Core.Domain;
using WebFor.Core.Repository;

namespace WebFor.Web.Mapper
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
        UserProfileViewModel UserToUserProfileViewModel(ApplicationUser user);
        List<ArticleTagViewModel> ArticleTagCollectionToArticleTagViewModelCollection(List<ArticleTag> tags);
        List<ContactViewModel> ContactCollectionToContactViewModelCollection(List<Contact> contacts);
        SlideShow SlideShowViewModelToSlideShow(SlideShowViewModel slideShowViewModel, string imagePath);
        ApplicationUser UserProfileViewModelToUser(UserProfileViewModel viewModel);
        SiteOrder SiteOrderViewModelToSiteOrder(SiteOrderViewModel viewModel);
        Contact ContactViewModelToContact(ContactViewModel contactViewModel);
        List<PortfolioViewModel> PortfolioCollectionToPortfolioViewModelCollection(List<Portfolio> portfolios);
        SlideShowViewModelEdit SlideShowToSlideShowViewModelEdit(SlideShow model);
        Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName);
        PortfolioViewModel PortfolioToPorfolioViewModel(Portfolio model);
        SlideShow SlideShowViewModelEditToSlideShow(SlideShowViewModelEdit viewModel);
        PortfolioViewModelEdit PortfolioToPortfolioViewModelEdit(Portfolio model);
        Portfolio PortfolioViewModelEditToPortfolio(PortfolioViewModelEdit viewModel);
    }

    public class WebForMapper : IWebForMapper
    {

        private IUnitOfWork _uw;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public WebForMapper(IUnitOfWork uw, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _uw = uw;
            this.contextAccessor = contextAccessor;
            _userManager = userManager;
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
            cfg.CreateMap<ApplicationUser, UserProfileViewModel>();
            cfg.CreateMap<UserProfileViewModel, ApplicationUser>();
            cfg.CreateMap<SiteOrder, SiteOrderViewModel>();
            cfg.CreateMap<SiteOrderViewModel, SiteOrder>();

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
            //UserManager<ApplicationUser>.GetUserAsync(contextAccessor.HttpContext.User)
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == _userManager.GetUserId(contextAccessor.HttpContext.User));

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

        public UserProfileViewModel UserToUserProfileViewModel(ApplicationUser user)
        {
            return Mapper.Map<ApplicationUser, UserProfileViewModel>(user);
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

        public ApplicationUser UserProfileViewModelToUser(UserProfileViewModel viewModel)
        {
            return Mapper.Map<UserProfileViewModel, ApplicationUser>(viewModel);
        }

        public SiteOrder SiteOrderViewModelToSiteOrder(SiteOrderViewModel viewModel)
        {
            return Mapper.Map<SiteOrderViewModel, SiteOrder>(viewModel);
        }

        public Contact ContactViewModelToContact(ContactViewModel contactViewModel)
        {
            return Mapper.Map<ContactViewModel, Contact>(contactViewModel);
        }

        public List<PortfolioViewModel> PortfolioCollectionToPortfolioViewModelCollection(List<Portfolio> portfolios)
        {
            var viewModels = Mapper.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios);

            var viewModelWithCategory = viewModels.Select(
                v =>
                {
                    v.PortfolioCategoryList =
                        portfolios.Single(p => p.PortfolioId.Equals(v.PortfolioId))
                            .PortfolioCategory.Split(',')
                            .ToList();

                    return v;
                }
                ).ToList();

            return viewModelWithCategory;
        }

        public SlideShowViewModelEdit SlideShowToSlideShowViewModelEdit(SlideShow model)
        {
            return Mapper.Map<SlideShow, SlideShowViewModelEdit>(model);
        }

        public Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName)
        {
            var portfolio = Mapper.Map<PortfolioViewModel, Portfolio>(viewModel);

            portfolio.PortfolioThumbnail = thumbFileName;
            portfolio.PortfolioCategory = string.Join(",", viewModel.PortfolioCategoryList);

            return portfolio;
        }

        public PortfolioViewModel PortfolioToPorfolioViewModel(Portfolio model)
        {
            return Mapper.Map<Portfolio, PortfolioViewModel>(model);
        }

        public SlideShow SlideShowViewModelEditToSlideShow(SlideShowViewModelEdit viewModel)
        {
            return Mapper.Map<SlideShowViewModelEdit, SlideShow>(viewModel);
        }

        public PortfolioViewModelEdit PortfolioToPortfolioViewModelEdit(Portfolio model)
        {
            var portfolioViewModelEdit = Mapper.Map<Portfolio, PortfolioViewModelEdit>(model);

            portfolioViewModelEdit.CurrentThumbnail = model.PortfolioThumbnail;
            portfolioViewModelEdit.PortfolioCategoryList = model.PortfolioCategory.Split(',').ToList();

            return portfolioViewModelEdit;
        }

        public Portfolio PortfolioViewModelEditToPortfolio(PortfolioViewModelEdit viewModel)
        {
            var portfolio = Mapper.Map<PortfolioViewModelEdit, Portfolio>(viewModel);

            portfolio.PortfolioCategory = string.Join(",", viewModel.PortfolioCategoryList);

            return portfolio;
        }

        public ArticleComment ArticleCommentViewModelToArticleComment(ArticleCommentViewModel viewModel)
        {
            return Mapper.Map<ArticleCommentViewModel, ArticleComment>(viewModel);
        }

        public Article ArticleViewModelToArticle(ArticleViewModel articleViewModel)
        {
            return Mapper.Map<ArticleViewModel, Article>(articleViewModel);
        }
    }
}