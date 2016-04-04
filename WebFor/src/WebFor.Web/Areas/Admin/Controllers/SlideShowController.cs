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
using WebFor.Web.Areas.Admin.ViewModels.SlideShow;
using WebFor.Web.Services;
using WebFor.Core.Services.Shared;
using cloudscribe.Web.Pagination;

namespace WebFor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideShowController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private IFileUploader _fileUploader;
        private IFileDeleter _fileDeleter;
        private IFileUploadValidator _fileValidator;


        public SlideShowController(IUnitOfWork uw, IWebForMapper webForMapper, IFileUploader fileUploader, IFileDeleter fileDeleter, IFileUploadValidator fileValidator)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _fileUploader = fileUploader;
            _fileDeleter = fileDeleter;
            _fileValidator = fileValidator;
        }

        [HttpGet]
        public async Task<IActionResult> ManageSlideShow(int? page)
        {
            var slideShows = await _uw.SlideShowRepository.GetAllAsync();

            var slideShowsViewModel = _webForMapper.SlideShowCollectionToSlideShowCollectionViewModel(slideShows);

            var pageNumber = page ?? 1;

            var pagedSlideShow = slideShowsViewModel.ToPagedList(pageNumber - 1, 2);

            return View(pagedSlideShow);
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
            if (!ModelState.IsValid)
            {
                return View(slideShowViewModel);
            }

            if (!_fileValidator.ValidateUploadedFile(slideShowViewModel.SlideShowPictrureFile, Core.Enums.UploadFileType.Image, 4, ModelState))
            {
                return View(slideShowViewModel);
            }

            string fileName = await _fileUploader.UploadFile(slideShowViewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

            var slideShow = _webForMapper.SlideShowViewModelToSlideShow(slideShowViewModel, fileName);

            int addSlideShowResult = await _uw.SlideShowRepository.AddNewSlideShow(slideShow);

            if (addSlideShowResult > 0)
            {
                TempData["SlideShowMessage"] = "اسلاید شو با موفقیت ثبت شد.";
                return RedirectToAction("ManageSlideShow");
            }

            TempData["SlideShowMessage"] = "مشکلی در ثبت اسلاید شو پیش آمده، اسلاید شو با موفقیت ثبت نشد.";

            return RedirectToAction("ManageSlideShow");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(default(int)))
            {
                return HttpBadRequest();
            }

            var model = await _uw.SlideShowRepository.FindByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            var viewModel = _webForMapper.SlideShowToSlideShowViewModelEdit(model);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SlideShowViewModelEdit viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var slideshow = _webForMapper.SlideShowViewModelEditToSlideShow(viewModel);

            if (viewModel.SlideShowPictrureFile != null)
            {

                if (!_fileValidator.ValidateUploadedFile(viewModel.SlideShowPictrureFile, Core.Enums.UploadFileType.Image, 4, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.SlideShowRepository.FindByIdAsync(viewModel.SlideShowId);

                _uw.SlideShowRepository.Detach(model);

                _fileDeleter.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

                string newPictureName = await _fileUploader.UploadFile(viewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

                slideshow.SlideShowPictrure = newPictureName;
            }

            int slideShowUpdateResult = await _uw.SlideShowRepository.UpdateSlideShowAsync(slideshow);

            if (slideShowUpdateResult > 0)
            {
                TempData["SlideShowMessage"] = "اسلاید شو با موفقیت ویرایش شد.";

                return RedirectToAction("ManageSlideShow");
            }

            TempData["SlideShowMessage"] = "مشکلی در ویرایش اسلاید شو پیش آمده، لطفا دوباره تلاش کنید.";

            return RedirectToAction("ManageSlideShow");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.SlideShowRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "SlideShowNotFound" });
            }

            try
            {
                _fileDeleter.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

                int deleteSlideShowResult = await _uw.SlideShowRepository.DeleteSlideShowAsync(model);

                if (deleteSlideShowResult > 0)
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
    }
}
