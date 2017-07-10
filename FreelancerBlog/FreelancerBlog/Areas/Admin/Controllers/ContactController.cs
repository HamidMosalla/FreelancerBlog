using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cloudscribe.Web.Pagination;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;

        public ContactController(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ManageContact(int? page)
        {
            var contacts = await _uw.ContactRepository.GetAllAsync();

            var contactsViewModel = _mapper.Map<List<Contact>, List<ContactViewModel>>(contacts);

            var pageNumber = page ?? 1;

            var pagedContact = contactsViewModel.ToPagedList(pageNumber - 1, 20);

            return View(pagedContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteContact(int id)
        {
            if (id == default(int))
            {
                return Json(new { Status = "IdCannotBeNull" });
            }

            var model = await _uw.ContactRepository.FindByIdAsync(id);

            if (model == null)
            {
                return Json(new { Status = "ContactNotFound" });
            }

            int deleteContactResult = await _uw.ContactRepository.DeleteContactAsync(model);

            if (deleteContactResult > 0)
            {
                return Json(new { Status = "Deleted" });
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