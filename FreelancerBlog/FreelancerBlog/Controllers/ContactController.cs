using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Services.Shared;
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
        private readonly IMediator _mediator;

        public ContactController(IMapper mapper, IMediator mediator)
        {
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
            CaptchaResponse captchaResult = await _mediator.Send(new ValidateCaptchaQuery());

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