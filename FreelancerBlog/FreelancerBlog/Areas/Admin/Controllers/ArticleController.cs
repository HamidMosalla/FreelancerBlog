using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Commands.ArticleComments;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Core.Commands.ArticleTags;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Queries.Article;
using FreelancerBlog.Core.Queries.ArticleComments;
using FreelancerBlog.Core.Queries.Articles;
using FreelancerBlog.Core.Queries.ArticleTags;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.ViewModels.Article;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.JQuery.DataTables;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ArticleController : Controller
    {
        private readonly ICkEditorFileUploder _ckEditorFileUploader;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private readonly IFileManager _fileManager;

        public ArticleController(ICkEditorFileUploder ckEditorFileUploader, IFileManager fileManager, IMapper mapper, UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _ckEditorFileUploader = ckEditorFileUploader;
            _fileManager = fileManager;
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult ManageArticle() => View();

        [HttpGet]
        public async Task<DataTablesResult<ArticleViewModel>> GetArticleTableData(DataTablesParam dataTableParam)
        {
            var articles = await _mediator.Send(new GetAriclesQuery());

            var articleViewModels = articles.Select(a => new ArticleViewModel
            {
                ArticleId = a.ArticleId,
                ArticleDateModified = a.ArticleDateModified,
                ArticleDateCreated = a.ArticleDateCreated,
                ArticleStatus = a.ArticleStatus,
                ArticleTitle = a.ArticleTitle
            });

            return DataTablesResult.Create(articleViewModels, dataTableParam);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleComment()
        {
            var comments = await _mediator.Send(new GetAllCommentsQuery { });

            var commentsViewModel = _mapper.Map<List<ArticleComment>, List<ArticleCommentViewModel>>(comments.ToList());

            return View(commentsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ManageArticleTag()
        {
            var tags = await _mediator.Send(new GetAllArticleTagsQuery { });

            var tagsViewModel = _mapper.Map<List<ArticleTag>, List<ArticleTagViewModel>>(tags.ToList());

            return View(tagsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteArticleComment(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _mediator.Send(new ArticleCommentByIdQuery { ArticleCommentId = id });

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            await _mediator.Send(new DeleteArticleCommentCommand { ArticleComment = model });

            return Json(new { Status = "Deleted" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteArticleTag(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _mediator.Send(new FindArticleTagByIdQuery { ArticleTagId = id });

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            await _mediator.Send(new DeleteArticleTagCommand { ArticleTag = model });

            return Json(new { Status = "Deleted" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeArticleCommentApprovalStatus(int commentId)
        {
            if (commentId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _mediator.Send(new ArticleCommentByIdQuery { ArticleCommentId = commentId });

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            await _mediator.Send(new ToggleArticleCommentApprovalCommand { ArticleComment = model });

            return Json(new { Status = "Success" });
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

            var result = await _mediator.Send(new CreateNewArticleCommand { Article = model, ArticleTags = viewModel.ArticleTags });

            if (result.All(r => r != ArticleStatus.ArticleCreateSucess))
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

            var article = await _mediator.Send(new ArticleByArticleIdQuery { ArticleId = id });

            if (article == null)
            {
                return NotFound();
            }

            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(article);
            articleViewModel.ArticleTags = await _mediator.Send(new TagsByArticleIdQuery { ArticleId = article.ArticleId });
            articleViewModel.ArticleTagsList = await _mediator.Send(new GetCurrentArticleTagsQuery { ArticleId = article.ArticleId });
            articleViewModel.SumOfRating = articleViewModel.ArticleRatings.Sum(a => a.ArticleRatingScore) / articleViewModel.ArticleRatings.Count;
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == _userManager.GetUserId(User));

            return View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var article = _mapper.Map<ArticleViewModel, Article>(viewModel);

            List<ArticleStatus> result = await _mediator.Send(new EditArticleCommand { Article = article, ArticleTags = viewModel.ArticleTags });

            if (result.All(r => r != ArticleStatus.ArticleEditSucess))
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

            var model = await _mediator.Send(new ArticleCommentByIdQuery { ArticleCommentId = commentId });

            if (model == null)
            {
                return Json(new { Status = "ArticleCommentNotFound" });
            }

            await _mediator.Send(new EditArticleCommentCommand { ArticleComment = model, NewCommentBody = newCommentBody });

            return Json(new { Status = "Success" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditArticleTag(int tagId, string newTagName)
        {
            if (tagId == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _mediator.Send(new FindArticleTagByIdQuery { ArticleTagId = tagId });

            if (model == null)
            {
                return Json(new { Status = "ArticleTagNotFound" });
            }

            await _mediator.Send(new EditArticleTagCommand { ArticleTag = model, NewTagName = newTagName });

            return Json(new { Status = "Success" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }
            var model = await _mediator.Send(new ArticleByArticleIdQuery { ArticleId = id });

            if (model == null)
            {
                return Json(new { Status = "ArticleNotFound" });
            }

            _fileManager.DeleteEditorImages(model.ArticleBody, new List<string> { "Files", "ArticleUploads" });

            await _mediator.Send(new DeleteArticleCommand { Article = model });

            return Json(new { Status = "Deleted" });
        }

        public async Task<IActionResult> TagLookup()
        {
            var model = await _mediator.Send(new GetAllTagNamesQuery());

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CkEditorFileUploder(IFormFile file, string ckEditorFuncNum)
        {
            string htmlResult = await _ckEditorFileUploader.UploadFromCkEditorAsync(file, new List<string> { "images", "blog" }, ckEditorFuncNum);

            return Content(htmlResult, "text/html");
        }
    }
}
