using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Web.Pagination;
using Microsoft.AspNet.Mvc;
using WebFor.Core.Repository;
using WebFor.Core.Services.ArticleServices;
using WebFor.Web.Services;

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
    }
}
