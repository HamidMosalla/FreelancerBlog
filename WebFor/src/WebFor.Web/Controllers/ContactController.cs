using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.Contact;
using cloudscribe.Web.Pagination;

namespace WebFor.Web.Controllers
{
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel, bool isJavascriptEnabled)
        {

            if (ModelState.IsValid)
            {

                if (isJavascriptEnabled)
                {
                    var contact = _webForMapper.ContactViewModelToContact(contactViewModel);

                    int addContactResult = await _uw.ContactRepository.AddNewContactAsync(contact);

                    if (addContactResult > 0)
                    {
                        return Json(new {Status = "Success"});
                    }

                    return Json(new {Staus = "ProblematicSubmit"});
                }

                var contactWioutJavascript = _webForMapper.ContactViewModelToContact(contactViewModel);

                int addContactResultWioutJavascript = await _uw.ContactRepository.AddNewContactAsync(contactWioutJavascript);

                if (addContactResultWioutJavascript > 0)
                {
                    return View("Success");
                }

                TempData["CreateContactMessage"] = "مشکلی در ثبت پیغام شما پیش آمده، لطفا دوباره تلاش کنید، اگر مشکل حل نشد، با مدیر سایت تماس بگیرید.";

                return View(contactViewModel);
            }

            return View(contactViewModel);
        }
    }
}
