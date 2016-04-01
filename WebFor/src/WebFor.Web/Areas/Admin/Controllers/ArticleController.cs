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
using WebFor.Web.Services;
using System.Linq;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.ArticleServices;
using WebFor.Core.Services.Shared;
using cloudscribe.Web.Pagination;
using WebFor.Core.Enums;

namespace WebFor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private IUnitOfWork _uw;
        private ICkEditorFileUploder _ckEditorFileUploader;
        private IWebForMapper _webForMapper;
        private IArticleCreator _articleCreator;
        private IArticleEditor _articleEditor;

        public ArticleController(IUnitOfWork uw, ICkEditorFileUploder ckEditorFileUploader, IWebForMapper webForMapper, IArticleCreator articleCreator, IArticleEditor articleEditor)
        {
            _uw = uw;
            _ckEditorFileUploader = ckEditorFileUploader;
            _webForMapper = webForMapper;
            _articleCreator = articleCreator;
            _articleEditor = articleEditor;
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticle(int? page)
        {
            var articles = await _uw.ArticleRepository.GetAllAsync();

            var articlesViewModel = _webForMapper.ArticleCollectionToArticleViewModelCollection(articles);

            var pageNumber = page ?? 1;

            var pagedArticle = articlesViewModel.ToPagedList(pageNumber - 1, 2);

            return View(pagedArticle);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleComment(int? page)
        {
            var comments = await _uw.ArticleCommentRepository.GetAllAsync();

            var commentsViewModel = _webForMapper.ArticleCommentCollectionToArticleCommentViewModelCollection(comments);

            var pageNumber = page ?? 1;

            var pagedArticleComment = commentsViewModel.ToPagedList(pageNumber - 1, 200);

            return View(pagedArticleComment);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleTag(int? page)
        {
            var tags = await _uw.ArticleTagRepository.GetAllAsync();

            var tagsViewModel = _webForMapper.ArticleTagCollectionToArticleTagViewModelCollection(tags);

            var pageNumber = page ?? 1;

            var pagedArticleTag = tagsViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedArticleTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteArticleComment(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleCommentRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            try
            {
                int deleteArticleResult = await _uw.ArticleCommentRepository.DeleteArticleCommentAsync(model);

                if (deleteArticleResult > 0)
                {
                    return Json(new { Status = "Deleted" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteArticleTag(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleTagRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            try
            {
                int deleteArticleTagResult = await _uw.ArticleTagRepository.DeleteArticleTagAsync(model);

                if (deleteArticleTagResult > 0)
                {
                    return Json(new { Status = "Deleted" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeArticleCommentApprovalStatus(int commentId)
        {
            if (commentId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleCommentRepository.FindByIdAsync(commentId);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            try
            {
                int toggleArticleCommentApprovalResult = await _uw.ArticleCommentRepository.ToggleArticleCommentApproval(model);

                if (toggleArticleCommentApprovalResult > 0)
                {
                    return Json(new { Status = "Success" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
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

                List<ArticleStatus> result = await _articleCreator.CreateNewArticleAsync(model, viewModel.ArticleTags);

                if (result.Any(r => r == ArticleStatus.ArticleCreateSucess))
                {
                    TempData["ViewMessage"] = "مقاله با موفقیت ثبت شد.";

                    if (result.Any(r => r == ArticleStatus.ArticleTagCreateSucess))
                    {
                        TempData["ArticleTagCreateMessage"] = "تگ های جدید با موفقیت ثبت شدند.";
                    }

                    if (result.Any(r => r == ArticleStatus.ArticleArticleTagsCreateSucess))
                    {
                        TempData["ArticleArticleTagCreateMessage"] = "تگ ها با موفقیت به این مقاله اضافه شدند.";
                    }

                    return RedirectToAction("ManageArticle", "Article");
                }

                TempData["ViewMessage"] = "مشکلی در ثبت مقاله پیش آمده، مقاله با موفقیت ثبت نشد.";

                return RedirectToAction("ManageArticle", "Article");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                //return new HttpStatusCodeResult(StatusCodes.Status400BadRequest);
                return HttpBadRequest();
            }

            var article = await _uw.ArticleRepository.FindByIdAsync(id);

            if (article == null)
            {
                //return new HttpStatusCodeResult(StatusCodes.Status404NotFound);
                return HttpNotFound();
            }

            var articleViewModel = await _webForMapper.ArticleToArticleViewModelWithTagsAsync(article);

            return View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var article = _webForMapper.ArticleViewModelToArticle(viewModel);

                List<ArticleStatus> result = await _articleEditor.EditArticleAsync(article, viewModel.ArticleTags);

                if (result.Any(r => r == ArticleStatus.ArticleEditSucess))
                {
                    TempData["ViewMessage"] = "مقاله با موفقیت ویرایش شد.";

                    if (result.Any(r => r == ArticleStatus.ArticleTagCreateSucess))
                    {
                        TempData["ArticleTagCreateMessage"] = "تگ های جدید با موفقیت ثبت شدند.";
                    }

                    if (result.Any(r => r == ArticleStatus.ArticleArticleTagsCreateSucess))
                    {
                        TempData["ArticleArticleTagCreateMessage"] = "تگ ها با موفقیت به این مقاله اضافه شدند.";
                    }

                    if (result.Any(r => r == ArticleStatus.ArticleRemoveTagsFromArticleSucess))
                    {
                        TempData["ArticleArticleTagRemoveFromArticle"] = "تگ ها با موفقیت از این مقاله حذف شدند.";
                    }

                    return RedirectToAction("ManageArticle", "Article");
                }

                TempData["ViewMessage"] = "مشکلی در ویرایش مقاله پیش آمده، مقاله با موفقیت ثبت نشد.";

                return RedirectToAction("ManageArticle", "Article");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditArticleComment(int commentId, string newCommentBody)
        {
            if (commentId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleCommentRepository.FindByIdAsync(commentId);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            try
            {
                int editCommentResult = await _uw.ArticleCommentRepository.EditArticleCommentAsync(model, newCommentBody);

                if (editCommentResult > 0)
                {
                    return Json(new { Status = "Success" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditArticleTag(int tagId, string newTagName)
        {
            if (tagId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleTagRepository.FindByIdAsync(tagId);

            if (model == null)
            {
                return Json(new { Status = "ArticleTagNotFound" });
            }

            try
            {
                int editArticleTagResult = await _uw.ArticleTagRepository.EditArticleTagAsync(model, newTagName);

                if (editArticleTagResult > 0)
                {
                    return Json(new { Status = "Success" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }
            var model = await _uw.ArticleRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ArticleNotFound" });
            }

            try
            {
                int deleteArticleResult = await _uw.ArticleRepository.DeleteArticleAsync(model);

                if (deleteArticleResult > 0)
                {
                    return Json(new { Status = "Deleted" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
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
