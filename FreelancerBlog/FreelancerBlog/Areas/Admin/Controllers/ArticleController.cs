using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cloudscribe.Web.Pagination;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.ArticleServices;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork _uw;
        private readonly ICkEditorFileUploder _ckEditorFileUploader;
        private readonly IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;
        private IArticleServices _articleServices;
        private readonly IFileManager _fileManager;

        public ArticleController(IUnitOfWork uw, ICkEditorFileUploder ckEditorFileUploader, IFreelancerBlogMapper freelancerBlogMapper, IArticleServices articleServices, IFileManager fileManager, IMapper mapper)
        {
            _uw = uw;
            _ckEditorFileUploader = ckEditorFileUploader;
            _freelancerBlogMapper = freelancerBlogMapper;
            _articleServices = articleServices;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticle(int? page)
        {
            var articles = await _uw.ArticleRepository.GetAllAsync();

            var articlesViewModel = _mapper.Map<List<Article>, List<ArticleViewModel>>(articles); ;

            var pageNumber = page ?? 1;

            var pagedArticle = articlesViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedArticle);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleComment(int? page)
        {
            var comments = await _uw.ArticleRepository.GetAllCommentAsync();

            var commentsViewModel = _mapper.Map<List<ArticleComment>, List<ArticleCommentViewModel>>(comments);

            var pageNumber = page ?? 1;

            var pagedArticleComment = commentsViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedArticleComment);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleTag(int? page)
        {
            var tags = await _uw.ArticleRepository.GetAllArticleTagsAsync();

            var tagsViewModel = _mapper.Map<List<ArticleTag>, List<ArticleTagViewModel>>(tags);

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

            var model = await _uw.ArticleRepository.FindCommentByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            int deleteArticleResult = await _uw.ArticleRepository.DeleteArticleCommentAsync(model);

            if (deleteArticleResult > 0)
            {
                return Json(new { Status = "Deleted" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteArticleTag(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleRepository.FindArticleTagByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            int deleteArticleTagResult = await _uw.ArticleRepository.DeleteArticleTagAsync(model);

            if (deleteArticleTagResult > 0)
            {
                return Json(new { Status = "Deleted" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeArticleCommentApprovalStatus(int commentId)
        {
            if (commentId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleRepository.FindCommentByIdAsync(commentId);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            int toggleArticleCommentApprovalResult = await _uw.ArticleRepository.ToggleArticleCommentApproval(model);

            if (toggleArticleCommentApprovalResult > 0)
            {
                return Json(new { Status = "Success" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
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
            if (!ModelState.IsValid) return View(viewModel);

            var model = _mapper.Map<ArticleViewModel, Article>(viewModel);

            List<ArticleStatus> result = await _articleServices.CreateNewArticleAsync(model, viewModel.ArticleTags);

            if (!result.Any(r => r == ArticleStatus.ArticleCreateSucess))
            {
                TempData["ViewMessage"] = "مشکلی در ثبت مقاله پیش آمده، مقاله با موفقیت ثبت نشد.";

                return RedirectToAction("ManageArticle", "Article");
            }

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

            var articleViewModel = await _freelancerBlogMapper.ArticleToArticleViewModelWithTagsAsync(article);

            return View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var article = _mapper.Map<ArticleViewModel, Article>(viewModel);

            List<ArticleStatus> result = await _articleServices.EditArticleAsync(article, viewModel.ArticleTags);

            if (!result.Any(r => r == ArticleStatus.ArticleEditSucess))
            {
                TempData["ViewMessage"] = "مشکلی در ویرایش مقاله پیش آمده، مقاله با موفقیت ثبت نشد.";

                return RedirectToAction("ManageArticle", "Article");
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditArticleComment(int commentId, string newCommentBody)
        {
            if (commentId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleRepository.FindCommentByIdAsync(commentId);

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            int editCommentResult = await _uw.ArticleRepository.EditArticleCommentAsync(model, newCommentBody);

            if (editCommentResult > 0)
            {
                return Json(new { Status = "Success" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditArticleTag(int tagId, string newTagName)
        {
            if (tagId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ArticleRepository.FindArticleTagByIdAsync(tagId);

            if (model == null)
            {
                return Json(new { Status = "ArticleTagNotFound" });
            }

            int editArticleTagResult = await _uw.ArticleRepository.EditArticleTagAsync(model, newTagName);

            if (editArticleTagResult > 0)
            {
                return Json(new { Status = "Success" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
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

            _fileManager.DeleteEditorImages(model.ArticleBody, new List<string> { "Files", "ArticleUploads" });

            int deleteArticleResult = await _uw.ArticleRepository.DeleteArticleAsync(model);

            if (deleteArticleResult > 0)
            {
                return Json(new { Status = "Deleted" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }

        public async Task<IActionResult> TagLookup()
        {
            var model = await _uw.ArticleRepository.GetAllTagNamesArrayAsync();

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CkEditorFileUploder(IFormFile file, string ckEditorFuncNum)
        {
            string htmlResult =
                await
                    _ckEditorFileUploader.UploadFromCkEditorAsync(file, new List<string> {"images", "blog"},
                        ckEditorFuncNum);

            return Content(htmlResult, "text/html");
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}
