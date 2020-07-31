using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.Commands.Data.Portfolios;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Portfolio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PortfolioController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IFileManager _fileManager;

        public PortfolioController(IFileManager fileManager, IMapper mapper, IMediator mediator)
        {
            _fileManager = fileManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ManagePortfolio()
        {
            var portfolios = await _mediator.Send(new GetAllPortfoliosQuery());

            var portfoliosViewModel = _mapper.Map<IQueryable<Portfolio>, List<PortfolioViewModel>>(portfolios);

            portfoliosViewModel.ForEach(v => v.PortfolioCategoryList = portfolios.Single(p => p.PortfolioId == v.PortfolioId).PortfolioCategory.Split(',').ToList());

            return View(portfoliosViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(PortfolioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (!_fileManager.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, UploadFileType.Image, 4, ModelState)) return View(viewModel);

            string fileName = await _fileManager.UploadFileAsync(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

            var portfolio = _mapper.Map<PortfolioViewModel, Portfolio>(viewModel);

            portfolio.PortfolioThumbnail = fileName;
            portfolio.PortfolioCategory = string.Join(",", viewModel.PortfolioCategoryList);

            await _mediator.Send(new AddNewPortfolioCommand { Portfolio = portfolio });

            TempData["PonrtfolioMessage"] = "پورتفولیو با موفقیت ثبت شد.";
            return RedirectToAction("ManagePortfolio");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == default(int)) return BadRequest();

            var model = await _mediator.Send(new PortfolioByIdQuery { PortfolioId = id });

            if (model == null) return NotFound();

            var viewModel = _mapper.Map<Portfolio, PortfolioViewModelEdit>(model);

            viewModel.CurrentThumbnail = model.PortfolioThumbnail;
            viewModel.PortfolioCategoryList = model.PortfolioCategory.Split(',').ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioViewModelEdit viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var portfolio = _mapper.Map<PortfolioViewModelEdit, Portfolio>(viewModel);

            portfolio.PortfolioCategory = string.Join(",", viewModel.PortfolioCategoryList);

            if (viewModel.PortfolioThumbnailFile != null)
            {

                if (!_fileManager.ValidateUploadedFile(viewModel.PortfolioThumbnailFile, UploadFileType.Image, 4, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _mediator.Send(new PortfolioByIdQuery { PortfolioId = viewModel.PortfolioId });

                FileStatus fileDeleteResult = _fileManager.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

                TempData["FileStatus"] = fileDeleteResult == FileStatus.DeleteSuccess ? "FileDeleteSuccess" : "FileNotFound";

                string newThumbName = await _fileManager.UploadFileAsync(viewModel.PortfolioThumbnailFile, new List<string> { "images", "portfolio", "thumb" });

                portfolio.PortfolioThumbnail = newThumbName;
            }

            await _mediator.Send(new UpdatePortfolioCommand { Portfolio = portfolio });

            TempData["PonrtfolioMessage"] = "پورتفولیو با موفقیت ویرایش شد.";
            return RedirectToAction("ManagePortfolio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            if (id == default(int)) return Json(new { Status = "IdCannotBeNull" });

            var model = await _mediator.Send(new PortfolioByIdQuery { PortfolioId = id });

            if (model == null) return Json(new { Status = "PortfolioNotFound" });

            FileStatus fileDeleteResult = _fileManager.DeleteFile(model.PortfolioThumbnail, new List<string> { "images", "portfolio", "thumb" });

            _fileManager.DeleteEditorImages(model.PortfolioBody, new List<string> { "images", "portfolio", "full" });

            await _mediator.Send(new DeletePortfolioCommand { Portfolio = model });

            return Json(new { Status = "Deleted", fileStatus = fileDeleteResult == FileStatus.DeleteSuccess ? "FileDeleteSuccess" : "FileDeleteFailure" });
        }

        [HttpPost]
        public async Task<IActionResult> CkEditorFileUploder(IFormFile file, string ckEditorFuncNum)
        {
            string htmlResult = await _fileManager.UploadFromCkEditorAsync(file, new List<string> { "images", "portfolio", "full" }, ckEditorFuncNum);

            return Content(htmlResult, "text/html");
        }
    }
}