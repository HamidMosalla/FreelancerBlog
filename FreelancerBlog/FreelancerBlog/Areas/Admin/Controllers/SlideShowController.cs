using System.Collections.Generic;
using System.Threading.Tasks;
using cloudscribe.Web.Pagination;
using FreelancerBlog.Areas.Admin.ViewModels.SlideShow;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class SlideShowController : Controller
    {
        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private IFileManager _fileManager;


        public SlideShowController(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, IFileManager fileManager)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> ManageSlideShow(int? page)
        {
            var slideShows = await _uw.SlideShowRepository.GetAllAsync();

            var slideShowsViewModel = _freelancerBlogMapper.SlideShowCollectionToSlideShowCollectionViewModel(slideShows);

            var pageNumber = page ?? 1;

            var pagedSlideShow = slideShowsViewModel.ToPagedList(pageNumber - 1, 20);

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

            if (!_fileManager.ValidateUploadedFile(slideShowViewModel.SlideShowPictrureFile, UploadFileType.Image, 4, ModelState))
            {
                return View(slideShowViewModel);
            }

            string fileName = await _fileManager.UploadFileAsync(slideShowViewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

            var slideShow = _freelancerBlogMapper.SlideShowViewModelToSlideShow(slideShowViewModel, fileName);

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
                return BadRequest();
            }

            var model = await _uw.SlideShowRepository.FindByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _freelancerBlogMapper.SlideShowToSlideShowViewModelEdit(model);

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

            var slideshow = _freelancerBlogMapper.SlideShowViewModelEditToSlideShow(viewModel);

            if (viewModel.SlideShowPictrureFile != null)
            {

                if (!_fileManager.ValidateUploadedFile(viewModel.SlideShowPictrureFile, UploadFileType.Image, 4, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.SlideShowRepository.FindByIdAsync(viewModel.SlideShowId);

                _uw.SlideShowRepository.Detach(model);

                FileStatus fileDeleteResult = _fileManager.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

                TempData["FileDeleteStatus"] = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure";

                string newPictureName = await _fileManager.UploadFileAsync(viewModel.SlideShowPictrureFile, new List<string> { "images", "slider" });

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

            FileStatus fileDeleteResult = _fileManager.DeleteFile(model.SlideShowPictrure, new List<string> { "images", "slider" });

            int deleteSlideShowResult = await _uw.SlideShowRepository.DeleteSlideShowAsync(model);

            if (deleteSlideShowResult > 0)
            {
                return Json(new { Status = "Deleted", fileDeleteStatus = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure" });
            }

            return Json(new { Status = "NotDeletedSomeProblem" });
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}
