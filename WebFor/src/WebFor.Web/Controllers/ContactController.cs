using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Web.ViewModels.Contact;
using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebFor.Web.Mapper;
using WebFor.Core.Services.Shared;
using WebFor.Core.Types;

namespace WebFor.Web.Controllers
{
    public class ContactController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private ICaptchaValidator _captchaValidator;
        private IConfigurationBinderWrapper _configurationWrapper;

        public ContactController(IUnitOfWork uw, IWebForMapper webForMapper, ICaptchaValidator captchaValidator, IConfigurationBinderWrapper configurationWrapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
            _captchaValidator = captchaValidator;
            _configurationWrapper = configurationWrapper;
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
                var contact = _webForMapper.ContactViewModelToContact(contactViewModel);

                int addContactResult = await _uw.ContactRepository.AddNewContactAsync(contact);

                if (addContactResult > 0)
                {
                    return Json(new { Status = "Success" });
                }

                return Json(new { Staus = "ProblematicSubmit" });
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

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }
    }
}
