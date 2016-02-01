using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using WebFor.Web.Areas.Admin.ViewModels;
using WebFor.Web.Areas.Admin.ViewModels.Article;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.ArticleServices;
using WebFor.Core.Services.Shared;
using WebFor.Web.Services;

namespace WebFor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private IUnitOfWork _uw;
        private ICkEditorFileUploder _ckEditorFileUploader;
        private IWebForMapper _webForMapper;
        private IArticleCreator _articleCreator;

        public ArticleController(IUnitOfWork uw, ICkEditorFileUploder ckEditorFileUploader, IWebForMapper webForMapper, IArticleCreator articleCreator)
        {
            _uw = uw;
            _ckEditorFileUploader = ckEditorFileUploader;
            _webForMapper = webForMapper;
            _articleCreator = articleCreator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _webForMapper.ArticleViewModelToArticle(viewModel);

                int resultl = await _articleCreator.CreateNewArticle(model, viewModel.ArticleTags);

                if (resultl > 0)
                {
                    TempData["ViewMessage"] = "مقاله با موفقیت ثبت شد.";

                    return RedirectToAction("Index", "Article");
                }

                TempData["ViewMessage"] = "مشکلی در ثبت مقاله پیش آمده، مقاله با موفقیت ثبت نشد.";

                return RedirectToAction("Index", "Article");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Article article)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }


        public async Task<IActionResult> TagLookup()
        {
            var model = await _uw.ArticleTagRepository.GetAllTagNamesArrayAsync();

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CkEditorFileUploder(IFormFile upload, string CKEditorFuncNum, string CKEditor,
           string langCode)
        {
            string vOutput = await _ckEditorFileUploader.UploadAsync(
                                   upload,
                                   new List<string>() { "Files", "ArticleUploads" },
                                   "/Files/ArticleUploads/",
                                   CKEditorFuncNum,
                                   CKEditor,
                                   langCode);

            return Content(vOutput, "text/html");
        }
    }
}
