using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;
using WebFor.Web.Services;
using System.Security.Claims;

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


        public ProfileController(IUnitOfWork uw, IWebForMapper webForMapper, IFileUploader fileUploader, IFileDeleter fileDeleter, IFileUploadValidator fileValidator)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _fileUploader = fileUploader;
            _fileDeleter = fileDeleter;
            _fileValidator = fileValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {

            var user = await _uw.UserRepository.FindByIdAsync(User.GetUserId());

            var userProfileViewModel = _webForMapper.UserToUserProfileViewModel(user);

            return View(userProfileViewModel);
        }

    }
}
