using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.SlideShow;
using FreelancerBlog.Core.Commands.Data.SlideShows;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Core.Services.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class SlideShowController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IFileManager _fileManager;

        public SlideShowController(IFileManager fileManager, IMediator mediator, IMapper mapper)
        {
            _fileManager = fileManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ManageSlideShow()
        {
            var slideShows = await _mediator.Send(new AllSlideShowsQuery());

            var slideShowsViewModel = _mapper.Map<IQueryable<SlideShow>, List<SlideShowViewModel>>(slideShows);

            return View(slideShowsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideShowViewModel slideShowViewModel)
        {
            if (!ModelState.IsValid) return View(slideShowViewModel);

            if (!_fileManager.ValidateUploadedFile(slideShowViewModel.SlideShowPictrureFile, UploadFileType.Image, 4, ModelState))
            {
                return View(slideShowViewModel);
            }

            string fileName = await _fileManager.UploadFileAsync(slideShowViewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

            var slideShow = _mapper.Map<SlideShowViewModel, SlideShow>(slideShowViewModel);

            slideShow.SlideShowPictrure = fileName;

            await _mediator.Send(new AddNewSlideShowCommand { SlideShow = slideShow });

            TempData["SlideShowMessage"] = "اسلاید شو با موفقیت ثبت شد.";
            return RedirectToAction("ManageSlideShow");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(default(int))) return BadRequest();

            var model = await _mediator.Send(new SlideShowByIdQuery { SlideShowId = id });

            if (model == null) return NotFound();

            var viewModel = _mapper.Map<SlideShow, SlideShowViewModelEdit>(model);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SlideShowViewModelEdit viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var slideshow = _mapper.Map<SlideShowViewModelEdit, SlideShow>(viewModel);

            if (viewModel.SlideShowPictrureFile == null)
            {
                await _mediator.Send(new UpdateSlideShowCommand { SlideShow = slideshow });

                return RedirectToAction("ManageSlideShow");
            }

            if (!_fileManager.ValidateUploadedFile(viewModel.SlideShowPictrureFile, UploadFileType.Image, 4, ModelState))
            {
                return View(viewModel);
            }

            var model = await _mediator.Send(new SlideShowByIdQuery { SlideShowId = viewModel.SlideShowId });

            //_uw.SlideShowRepository.Detach(model);

            FileStatus fileDeleteResult = _fileManager.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

            TempData["FileDeleteStatus"] = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure";

            string newPictureName = await _fileManager.UploadFileAsync(viewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

            slideshow.SlideShowPictrure = newPictureName;

            await _mediator.Send(new UpdateSlideShowCommand { SlideShow = slideshow });

            return RedirectToAction("ManageSlideShow");



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == default(int)) return Json(new { Status = "IdCannotBeNull" });

            var model = await _mediator.Send(new SlideShowByIdQuery { SlideShowId = id });

            if (model == null) return Json(new { Status = "SlideShowNotFound" });

            FileStatus fileDeleteResult = _fileManager.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

            await _mediator.Send(new DeleteSlideShowCommand { SlideShow = model });

            return Json(new { Status = "Deleted", fileDeleteStatus = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure" });
        }
    }
}