using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Web.ViewModels.Contact;
using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Authorization;
using WebFor.Web.Mapper;

namespace WebFor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public ContactController(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ManageContact(int? page)
        {
            var contacts = await _uw.ContactRepository.GetAllAsync();

            var contactsViewModel = _webForMapper.ContactCollectionToContactViewModelCollection(contacts);

            var pageNumber = page ?? 1;

            var pagedContact = contactsViewModel.ToPagedList(pageNumber - 1, 2);

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

            try
            {
                int deleteContactResult = await _uw.ContactRepository.DeleteContactAsync(model);

                if (deleteContactResult > 0)
                {
                    return Json(new { Status = "Deleted" });
                }

                return Json(new { Status = "NotDeletedSomeProblem" });
            }

            catch (Exception eX)
            {
                return Json(new { Status = "Error", eXMessage = eX.Message });
            }
        }
    }
}
