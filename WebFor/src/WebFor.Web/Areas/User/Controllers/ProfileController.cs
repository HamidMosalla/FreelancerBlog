using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;
using WebFor.Web.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFor.Web.Areas.User.ViewModels.Profile;
using WebFor.Core.Domain;

namespace WebFor.Web.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private IFileUploader _fileUploader;
        private IFileDeleter _fileDeleter;
        private IFileUploadValidator _fileValidator;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProfileController(IUnitOfWork uw, IWebForMapper webForMapper, IFileUploader fileUploader, IFileDeleter fileDeleter, IFileUploadValidator fileValidator, UserManager<ApplicationUser> userManager)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _fileUploader = fileUploader;
            _fileDeleter = fileDeleter;
            _fileValidator = fileValidator;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _uw.UserRepository.FindByIdAsync(_userManager.GetUserId(User));

            var userProfileViewModel = _webForMapper.UserToUserProfileViewModel(user);

            return View(userProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(UserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //there is no need to map here, since the issue with tracking
            //that is the identity system already track the logged in user
            //just pass the viewModel to the updateuser method.
            var user = _webForMapper.UserProfileViewModelToUser(viewModel);

            if (viewModel.UserAvatarFile != null)
            {

                if (!_fileValidator.ValidateUploadedFile(viewModel.UserAvatarFile, Core.Enums.UploadFileType.Image, .03, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _uw.UserRepository.FindByIdAsync(viewModel.Id);

                _uw.UserRepository.Detach(model);

                if (model.UserAvatar != null)
                {
                    _fileDeleter.DeleteFile(model.UserAvatar, new List<string> { "images", "user-avatar" });
                }

                string newAvatarName = await _fileUploader.UploadFile(viewModel.UserAvatarFile, new List<string> { "images", "user-avatar" });

                user.UserAvatar = newAvatarName;
            }

            int editUserResult = await _uw.UserRepository.UpdateUserProfileAsync(user);

            if (editUserResult > 0)
            {
                TempData["EditProfileMessage"] = "EditProfileSuccess";
                return RedirectToAction("Index", "Manage", new { Area = "" });
            }

            TempData["EditProfileMessage"] = "EditProfileFailed";
            return RedirectToAction("Index", "Manage", new {Area = ""});
        }

        [HttpGet("/User/Profile/ProfileDetail/{username?}")]
        [Authorize]
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

            var userProfileViewModel = _webForMapper.UserToUserProfileViewModel(user);

            return View(userProfileViewModel);
        }

    }
}
