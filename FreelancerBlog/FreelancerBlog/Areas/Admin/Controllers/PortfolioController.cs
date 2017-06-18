using System.Collections.Generic;
using System.Threading.Tasks;
using cloudscribe.Web.Pagination;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PortfolioController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private ICkEditorFileUploder _ckEditorFileUploader;
        private IFileManager _fileManager;

        public PortfolioController(IUnitOfWork uw, IWebForMapper webForMapper, IFileManager fileManager, ICkEditorFileUploder ckEditorFileUploader)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _fileManager = fileManager;
            _ckEditorFileUploader = ckEditorFileUploader;
        }

        [HttpGet]
        public async Task<IActionResult> ManagePortfolio(int? page)
        {
            var portfolios = await _uw.PortfolioRepository.GetAllAsync();

            var portfoliosViewModel = _webForMapper.PortfolioCollectionToPortfolioViewModelCollection(portfolios);

            var pageNumber = page ?? 1;

            var pagedPortfolios = portfoliosViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedPortfolios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(PortfolioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!_fileManager.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, UploadFileType.Image, 4, ModelState))
            {
                return View(viewModel);
            }

            string fileName = await _fileManager.UploadFileAsync(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

            var portfolio = _webForMapper.PortfolioViewModelToPorfolio(viewModel, fileName);

            int createPortfolioResult = await _uw.PortfolioRepository.AddNewPortfolio(portfolio);

            if (createPortfolioResult > 0)
            {
                TempData["PonrtfolioMessage"] = "پورتفولیو با موفقیت ثبت شد.";
                return RedirectToAction("ManagePortfolio");
            }

            TempData["PonrtfolioMessage"] = "مشکلی در ثبت پورتفولیو پیش آمده، اسلاید شو با موفقیت ثبت نشد.";

            return RedirectToAction("ManagePortfolio");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(default(int)))
            {
                return BadRequest();
            }

            var model = await _uw.PortfolioRepository.FindByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _webForMapper.PortfolioToPortfolioViewModelEdit(model);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioViewModelEdit viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var portfolio = _webForMapper.PortfolioViewModelEditToPortfolio(viewModel);

            if (viewModel.PortfolioThumbnailFile != null)
            {

                if (!_fileManager.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, UploadFileType.Image, 4, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.PortfolioRepository.FindByIdAsync(viewModel.PortfolioId);

                _uw.PortfolioRepository.Detach(model);

                FileStatus fileDeleteResult = _fileManager.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

                TempData["FileStatus"] = fileDeleteResult == FileStatus.DeleteSuccess
                    ? "FileDeleteSuccess"
                    : "FileNotFound";

                string newThumbName = await _fileManager.UploadFileAsync(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

                portfolio.PortfolioThumbnail = newThumbName;
            }

            int portfolioUpdateResult = await _uw.PortfolioRepository.UpdatePortfolioAsync(portfolio);

            if (portfolioUpdateResult > 0)
            {
                TempData["PonrtfolioMessage"] = "پورتفولیو با موفقیت ویرایش شد.";

                return RedirectToAction("ManagePortfolio");
            }

            TempData["PonrtfolioMessage"] = "مشکلی در ویرایش پورتفولیو پیش آمده، لطفا دوباره تلاش کنید.";

            return RedirectToAction("ManagePortfolio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.PortfolioRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "PortfolioNotFound" });
            }

            FileStatus fileDeleteResult = _fileManager.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

            _fileManager.DeleteEditorImages(model.PortfolioBody, new List<string> { "images", "portfolio", "full" });

            int deletePortfolioResult = await _uw.PortfolioRepository.DeletePortfolioAsync(model);

            if (deletePortfolioResult > 0)
            {
                return Json(new { Status = "Deleted", fileStatus = fileDeleteResult == FileStatus.DeleteSuccess ? "FileDeleteSuccess" : "FileDeleteFailure" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }

        [HttpPost]
        public async Task<IActionResult> CkEditorFileUploder(IFormFile file, string ckEditorFuncNum)
        {
            string htmlResult =
                await
                    _ckEditorFileUploader.UploadFromCkEditorAsync(file, new List<string> {"images", "portfolio", "full"},
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
