using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Areas.Admin.ViewModels.SlideShow;
using FreelancerBlog.Areas.User.ViewModels.Profile;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.ViewModels.Article;
using FreelancerBlog.ViewModels.Contact;
using FreelancerBlog.ViewModels.SiteOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.AutoMapper
{
    public interface IFreelancerBlogMapper
    {
        Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article);
        SlideShow SlideShowViewModelToSlideShow(SlideShowViewModel slideShowViewModel, string imagePath);
        List<PortfolioViewModel> PortfolioCollectionToPortfolioViewModelCollection(List<Portfolio> portfolios);
        Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName);
        PortfolioViewModelEdit PortfolioToPortfolioViewModelEdit(Portfolio model);
        Portfolio PortfolioViewModelEditToPortfolio(PortfolioViewModelEdit viewModel);
    }

    public class FreelancerBlogMapper : IFreelancerBlogMapper
    {

        private IUnitOfWork _uw;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public FreelancerBlogMapper(IUnitOfWork uw, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
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

        public async Task<ArticleViewModel> ArticleToArticleViewModelWithTagsAsync(Article article)
        {
            var articleViewModel = Mapper.Map<Article, ArticleViewModel>(article);

            articleViewModel.ArticleTags = await _uw.ArticleRepository.GetTagsByArticleIdAsync(article.ArticleId);
            articleViewModel.ArticleTagsList = await _uw.ArticleRepository.GetCurrentArticleTagsAsync(article.ArticleId);


            articleViewModel.SumOfRating = articleViewModel.ArticleRatings.Sum(a => a.ArticleRatingScore) / articleViewModel.ArticleRatings.Count;
            //UserManager<ApplicationUser>.GetUserAsync(contextAccessor.HttpContext.User)
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == _userManager.GetUserId(contextAccessor.HttpContext.User));

            return articleViewModel;
        }

        public SlideShow SlideShowViewModelToSlideShow(SlideShowViewModel slideShowViewModel, string imagePath)
        {
            var slideShow = Mapper.Map<SlideShowViewModel, SlideShow>(slideShowViewModel);

            slideShow.SlideShowPictrure = imagePath;

            return slideShow;
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

        public Portfolio PortfolioViewModelToPorfolio(PortfolioViewModel viewModel, string thumbFileName)
        {
            var portfolio = Mapper.Map<PortfolioViewModel, Portfolio>(viewModel);

            portfolio.PortfolioThumbnail = thumbFileName;
            portfolio.PortfolioCategory = string.Join(",", viewModel.PortfolioCategoryList);

            return portfolio;
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
    }
}