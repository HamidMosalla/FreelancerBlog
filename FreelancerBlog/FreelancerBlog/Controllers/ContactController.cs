using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Types;
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
        public IActionResult Create() => View("Create");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel, bool isJavascriptEnabled)
        {
            CaptchaResponse captchaResult = await _mediator.Send(new ValidateCaptchaQuery());

            if (captchaResult.Success != "true") return Json(new { status = "FailedTheCaptchaValidation" });

            if (!ModelState.IsValid) return View(contactViewModel);

            var contactWioutJavascript = _mapper.Map<ContactViewModel, Contact>(contactViewModel);

            await _mediator.Send(new AddNewContactCommand { Contact = contactWioutJavascript });

            if (isJavascriptEnabled) return Json(new { Status = "Success" });

            return View("Success");
        }
    }
}