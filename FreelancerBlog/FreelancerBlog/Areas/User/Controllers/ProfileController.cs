using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.User.ViewModels.Profile;
using FreelancerBlog.Core.Commands.Data.ApplicationUsers;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Queries.Data.ApplicationUsers;
using FreelancerBlog.Core.Services.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IFileManager _fileManager;

        public ProfileController(IFileManager fileManager, IMapper mapper, IMediator mediator)
        {
            _fileManager = fileManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _mediator.Send(new UserByIdQuery { User = User });

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

            var user = _mapper.Map<UserProfileViewModel, ApplicationUser>(viewModel);

            if (viewModel.UserAvatarFile != null)
            {

                if (!_fileManager.ValidateUploadedFile(viewModel.UserAvatarFile, UploadFileType.Image, .03, ModelState))
                {
                    return View(viewModel);
                }

                var model = await _mediator.Send(new UserByIdQuery { User = User });

                //_uw.UserRepository.Detach(model);

                if (model.UserAvatar != null)
                {
                    FileStatus fileDeleteResult = _fileManager.DeleteFile(model.UserAvatar, new List<string> { "images", "user-avatar" });

                    TempData["FileDeleteStatus"] = fileDeleteResult == FileStatus.DeleteSuccess ? "Success" : "Failure";
                }

                string newAvatarName = await _fileManager.UploadFileAsync(viewModel.UserAvatarFile, new List<string> { "images", "user-avatar" });

                user.UserAvatar = newAvatarName;
            }

            await _mediator.Send(new UpdateUserProfileCommand { ApplicationUser = user });

            TempData["EditProfileMessage"] = "EditProfileSuccess";
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

            var user = await _mediator.Send(new UserByUserNameQuery { UserName = userName });

            if (user == null)
            {
                return NotFound();
            }

            var userProfileViewModel = _mapper.Map<ApplicationUser, UserProfileViewModel>(user);

            return View(userProfileViewModel);
        }
    }
}