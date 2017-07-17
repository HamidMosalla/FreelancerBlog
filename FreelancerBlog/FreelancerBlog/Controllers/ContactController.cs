using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Commands.Contacts;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.ViewModels.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private ICaptchaValidator _captchaValidator;
        private IConfigurationBinderWrapper _configurationWrapper;

        public ContactController(ICaptchaValidator captchaValidator, IConfigurationBinderWrapper configurationWrapper, IMapper mapper, IMediator mediator)
        {
            _captchaValidator = captchaValidator;
            _configurationWrapper = configurationWrapper;
            _mapper = mapper;
            _mediator = mediator;
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

                await _mediator.Send(new AddNewContactCommand { Contact = contact });

                return Json(new { Status = "Success" });
            }

            var contactWioutJavascript = _mapper.Map<ContactViewModel, Contact>(contactViewModel);

            await _mediator.Send(new AddNewContactCommand { Contact = contactWioutJavascript });

            return View("Success");

            //ViewData["CreateContactMessage"] = "NothingToSaveOrThereWasAProblem";

            //return View("Create", contactViewModel);
        }
    }
}