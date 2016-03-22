using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Web.Pagination;
using Microsoft.AspNet.Mvc;
using WebFor.Core.Repository;
using WebFor.Core.Services.ArticleServices;
using WebFor.Web.Services;
using System.Security.Claims;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.Controllers
{
    public class ArticleController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public ArticleController(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var articles = await _uw.ArticleRepository.GetAllAsync();

            var articlesViewModel = _webForMapper.ArticleCollectionToArticleViewModelCollection(articles);

            var pageNumber = page ?? 1;

            var pagedArticle = articlesViewModel.ToPagedList(pageNumber - 1, 2);

            return View(pagedArticle);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            if (id == 0)
            {
                return HttpBadRequest();
            }

            var article = await _uw.ArticleRepository.FindByIdAsync(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            await _uw.ArticleRepository.IncreaseArticleViewCount(id);

            var articleViewModel = await _webForMapper.ArticleToArticleViewModelWithTagsAsync(article);

            return View(articleViewModel);
        }

        public async Task<JsonResult> RateArticle(int id, double rating)
        {
            var userId = User.GetUserId();

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { Status = "YouMustLogin" });
            }

            if (_uw.ArticleRatingRepository.IsRatedBefore(id, userId))
            {
                int updateRatingResult = await _uw.ArticleRatingRepository.UpdateArticleRating(id, rating, userId);

                if (updateRatingResult > 0)
                {
                    return Json(new { Status = "UpdatedSuccessfully" });
                }

                return Json(new { Status = "SomeProblemWithSubmit" });
            }

            int addRatingResult = await _uw.ArticleRatingRepository.AddRatingForArticle(id, rating, userId);

            if (addRatingResult > 0)
            {
                return Json(new { Status = "Success" });

            }

            return Json(new { Status = "SomeProblemWithSubmit" });

        }

        [HttpPost]
        public async Task<JsonResult> SubmitComment(ArticleCommentViewModel viewModel)
        {

            if (viewModel.ArticleCommentName == null || viewModel.ArticleCommentEmail == null ||
                viewModel.ArticleCommentBody == null)
            {
                return Json(new {Status = "CannotHaveEmptyArgument"});
            }

            var articleComment = _webForMapper.ArticleCommentViewModelToArticleComment(viewModel);

            int addArticleCommentResult = await _uw.ArticleCommentRepository.AddCommentToArticle(articleComment);

            if (addArticleCommentResult > 0)
            {
                return Json(new {Status = "Success"});
            }

            return Json(new { Status = "ProblematicSubmit" });
        }
    }
}
