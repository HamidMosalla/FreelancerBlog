using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class ContactController : Controller
    {
        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;
        private ICaptchaValidator _captchaValidator;
        private IConfigurationBinderWrapper _configurationWrapper;

        public ContactController(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, ICaptchaValidator captchaValidator, IConfigurationBinderWrapper configurationWrapper, IMapper mapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _captchaValidator = captchaValidator;
            _configurationWrapper = configurationWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel, bool isJavascriptEnabled)
        {
            CaptchaResponse captchaResult = await _captchaValidator.ValidateCaptchaAsync(_configurationWrapper.GetValue<string>("reChaptchaSecret:server-secret"));


            if (captchaResult.Success != "true")
            {
                return Json(new { status = "FailedTheCaptchaValidation" });
            }

            if (!ModelState.IsValid) return View(contactViewModel);

            if (isJavascriptEnabled)
            {
                var contact = _mapper.Map<ContactViewModel, Contact>(contactViewModel);

                int addContactResult = await _uw.ContactRepository.AddNewContactAsync(contact);

                if (addContactResult > 0)
                {
                    return Json(new { Status = "Success" });
                }

                return Json(new { Status = "ProblematicSubmit" });
            }

            var contactWioutJavascript = _mapper.Map<ContactViewModel, Contact>(contactViewModel);

            int addContactResultWioutJavascript = await _uw.ContactRepository.AddNewContactAsync(contactWioutJavascript);

            if (addContactResultWioutJavascript > 0)
            {
                return View("Success");
            }

            ViewData["CreateContactMessage"] = "NothingToSaveOrThereWasAProblem";

            return View("Create", contactViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }
    }
}
