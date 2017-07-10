using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cloudscribe.Web.Pagination;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FreelancerBlog.Controllers
{
    public class ArticleController : Controller
    {
        private IUnitOfWork _uw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private ICaptchaValidator _captchaValidator;
        private IConfiguration _configuration;

        public ArticleController(IUnitOfWork uw, UserManager<ApplicationUser> userManager, ICaptchaValidator captchaValidator, IConfiguration configuration, IMapper mapper)
        {
            _uw = uw;
            _mapper = mapper;
            _userManager = userManager;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var articles = await _uw.ArticleRepository.GetAllAsync();

            var articlesViewModel =  _mapper.Map<List<Article>, List<ArticleViewModel>>(articles);

            var pageNumber = page ?? 1;

            var pagedArticle = articlesViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedArticle);
        }

        [HttpGet]
        public async Task<IActionResult> Tag(int id, int? page)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var articles = await _uw.ArticleRepository.GetArticlesByTag(id);

            var articlesViewModel = _mapper.Map<List<Article>, List<ArticleViewModel>>(articles);

            var pageNumber = page ?? 1;

            var pagedArticle = articlesViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedArticle);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var article = await _uw.ArticleRepository.FindByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            await _uw.ArticleRepository.IncreaseArticleViewCount(id);

            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(article);
            articleViewModel.ArticleTags = await _uw.ArticleRepository.GetTagsByArticleIdAsync(article.ArticleId);
            articleViewModel.ArticleTagsList = await _uw.ArticleRepository.GetCurrentArticleTagsAsync(article.ArticleId);
            articleViewModel.SumOfRating = articleViewModel.ArticleRatings.Sum(a => a.ArticleRatingScore) / articleViewModel.ArticleRatings.Count;
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == _userManager.GetUserId(User));

            return View(articleViewModel);
        }

        public async Task<JsonResult> RateArticle(int id, double rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { Status = "YouMustLogin" });
            }

            var userId = _userManager.GetUserId(User);

            if (_uw.ArticleRepository.IsRatedBefore(id, userId))
            {
                int updateRatingResult = await _uw.ArticleRepository.UpdateArticleRating(id, rating, userId);

                if (updateRatingResult > 0)
                {
                    return Json(new { Status = "UpdatedSuccessfully" });
                }

                return Json(new { Status = "SomeProblemWithSubmit" });
            }

            int addRatingResult = await _uw.ArticleRepository.AddRatingForArticle(id, rating, userId);

            if (addRatingResult > 0)
            {
                return Json(new { Status = "Success" });
            }

            return Json(new { Status = "SomeProblemWithSubmit" });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitComment(ArticleCommentViewModel viewModel)
        {
            CaptchaResponse captchaResult = await _captchaValidator.ValidateCaptchaAsync(_configuration.GetValue<string>("reChaptchaSecret:server-secret"));

            if (captchaResult.Success != "true")
            {
                return Json(new { status = "FailedTheCaptchaValidation" });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { Status = "CannotHaveEmptyArgument" });
            }

            var articleComment = _mapper.Map<ArticleCommentViewModel, ArticleComment>(viewModel);

            int addArticleCommentResult = await _uw.ArticleRepository.AddCommentToArticle(articleComment);

            if (addArticleCommentResult > 0)
            {
                return Json(new { Status = "Success" });
            }

            return Json(new { Status = "ProblematicSubmit" });
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }
    }
}
