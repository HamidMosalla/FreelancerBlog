using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.Contact;
using cloudscribe.Web.Pagination;
using Microsoft.AspNet.Http;
using WebFor.Core.Services.Shared;
using WebFor.Web.Areas.Admin.ViewModels.Portfolio;

namespace WebFor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private ICkEditorFileUploder _ckEditorFileUploader;
        private IFileUploader _fileUploader;
        private IFileDeleter _fileDeleter;
        private IFileUploadValidator _fileValidator;

        public PortfolioController(IUnitOfWork uw, IWebForMapper webForMapper, IFileUploader fileUploader, IFileDeleter fileDeleter, IFileUploadValidator fileValidator, ICkEditorFileUploder ckEditorFileUploader)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _fileUploader = fileUploader;
            _fileDeleter = fileDeleter;
            _fileValidator = fileValidator;
            _ckEditorFileUploader = ckEditorFileUploader;
        }

        [HttpGet]
        public async Task<IActionResult> ManagePortfolio(int? page)
        {
            var portfolios = await _uw.PortfolioRepository.GetAllAsync();

            var portfoliosViewModel = _webForMapper.PortfolioCollectionToPortfolioViewModelCollection(portfolios);

            var pageNumber = page ?? 1;

            var pagedPortfolios = portfoliosViewModel.ToPagedList(pageNumber - 1, 2);

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

            if (!_fileValidator.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, Core.Enums.UploadFileType.Image, 4, ModelState))
            {
                return View(viewModel);
            }

            string fileName = await _fileUploader.UploadFile(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

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
                return HttpBadRequest();
            }

            var model = await _uw.PortfolioRepository.FindByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
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

                if (!_fileValidator.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, Core.Enums.UploadFileType.Image, 4, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.PortfolioRepository.FindByIdAsync(viewModel.PortfolioId);

                _uw.PortfolioRepository.Detach(model);

                _fileDeleter.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

                string newThumbName = await _fileUploader.UploadFile(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

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

            try
            {
                _fileDeleter.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

                int deletePortfolioResult = await _uw.PortfolioRepository.DeletePortfolioAsync(model);

                if (deletePortfolioResult > 0)
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
        public async Task<IActionResult> CkEditorFileUploder(IFormFile upload, string CKEditorFuncNum, string CKEditor,
           string langCode)
        {
            string vOutput = await _ckEditorFileUploader.UploadAsync(
                                   upload,
                                   new List<string>() { "images", "portfolio", "full" },
                                   "/images/portfolio/full/",
                                   CKEditorFuncNum,
                                   CKEditor,
                                   langCode);

            return Content(vOutput, "text/html");
        }
    }
}
