using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.User.ViewModels.Profile;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {
        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;
        private IFileManager _fileManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProfileController(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, IFileManager fileManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _fileManager = fileManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _uw.UserRepository.FindByIdAsync(_userManager.GetUserId(User));

            var userProfileViewModel = _mapper.Map<ApplicationUser, UserProfileViewModel>(user);

            return View(userProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //there is no need to map here, since the issue with tracking
            //that is the identity system already track the logged in user
            //just pass the viewModel to the updateuser method.
            var user = _mapper.Map<UserProfileViewModel, ApplicationUser>(viewModel);

            if (viewModel.UserAvatarFile != null)
            {

                if (!_fileManager.ValidateUploadedFile(viewModel.UserAvatarFile, UploadFileType.Image, .03, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.UserRepository.FindByIdAsync(viewModel.Id);

                _uw.UserRepository.Detach(model);

                if (model.UserAvatar != null)
                {
                    FileStatus fileDeleteResult = _fileManager.DeleteFile(model.UserAvatar, new List<string> { "images", "user-avatar" });

                    TempData["FileDeleteStatus"] = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure";
                }

                string newAvatarName = await _fileManager.UploadFileAsync(viewModel.UserAvatarFile, new List<string> { "images", "user-avatar" });

                user.UserAvatar = newAvatarName;
            }

            int editUserResult = await _uw.UserRepository.UpdateUserProfileAsync(user);

            if (editUserResult > 0)
            {
                TempData["EditProfileMessage"] = "EditProfileSuccess";
                return RedirectToAction("Index", "Manage", new { Area = "" });
            }

            TempData["EditProfileMessage"] = "EditProfileFailed";
            return RedirectToAction("Index", "Manage", new { Area = "" });
        }

        [HttpGet("/User/Profile/ProfileDetail/{username?}")]
        [AllowAnonymous]
        public async Task<IActionResult> ProfileDetail(string userName)
        {
            if (userName == null)
            {
                return BadRequest();
            }

            var user = await _uw.UserRepository.FindByUserNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            var userProfileViewModel = _mapper.Map<ApplicationUser, UserProfileViewModel>(user);

            return View(userProfileViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }
    }
}
