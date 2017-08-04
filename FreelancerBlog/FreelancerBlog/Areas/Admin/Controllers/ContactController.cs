using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.ViewModels.Contact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        public readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ManageContact()
        {
            var contacts = await _mediator.Send(new GetAllContactQuery());

            var contactsViewModel = _mapper.Map<IQueryable<Contact>, List<ContactViewModel>>(contacts);

            return View(contactsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteContact(int id)
        {
            if (id == default(int)) return Json(new { Status = "IdCannotBeNull" });

            var model = await _mediator.Send(new ContactByIdQuery { ContactId = id });

            if (model == null) return Json(new { Status = "ContactNotFound" });

            await _mediator.Send(new DeleteContactCommand { Contact = model });

            return Json(new { Status = "Deleted" });
        }
    }
}