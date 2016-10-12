using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebFor.Core;
using WebFor.Core.Services;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;
using WebFor.Web.Mapper;
using WebFor.Web.ViewModels.SiteOrder;
using WebFor.Core.Services.Shared;
using WebFor.Core.Types;

namespace WebFor.Web.Controllers
{
    public class SiteOrderController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;
        private IPriceSpecCollectionFactory<PriceSpec, object> _priceSpecCollectionFactory;
        private IFinalPriceCalculator<PriceSpec> _finalPriceCalculator;
        private ICaptchaValidator _captchaValidator;
        private IConfiguration _configuration;

        public SiteOrderController(IPriceSpecCollectionFactory<PriceSpec, object> priceSpecCollectionFactory, IFinalPriceCalculator<PriceSpec> finalPriceCalculator, IUnitOfWork uw, IWebForMapper webForMapper, ICaptchaValidator captchaValidator, IConfiguration configuration)
        {
            _priceSpecCollectionFactory = priceSpecCollectionFactory;
            _finalPriceCalculator = finalPriceCalculator;
            _uw = uw;
            _webForMapper = webForMapper;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Index(SiteOrderViewModel viewModel)
        {
            CaptchaResponse captchaResult = await _captchaValidator.ValidateCaptchaAsync(_configuration.GetValue<string>("reChaptchaSecret:server-secret"));

            if (captchaResult.Success != "true")
            {
                return Json(new { status = "FailedTheCaptchaValidation" });
            }

            if (!ModelState.IsValid)
            {
                return Json(new {Status = "FormWasNotValid"});
            }

            var priceSpecCollection = _priceSpecCollectionFactory.BuildPriceSpecCollection(viewModel);

            var finalPrice = _finalPriceCalculator.CalculateFinalPrice(priceSpecCollection);

            var siteOrder = _webForMapper.SiteOrderViewModelToSiteOrder(viewModel);

            int addSiteOrderAsyncResult = await _uw.SiteOrderRepository.AddSiteOrderAsync(siteOrder);

            if (addSiteOrderAsyncResult > 0)
            {
                return Json(new { Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Success" });
            }

            return Json(new {Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Failed"});
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}
