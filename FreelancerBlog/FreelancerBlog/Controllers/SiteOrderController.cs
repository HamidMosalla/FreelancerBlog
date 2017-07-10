using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Infrastructure.Services.SiteOrderServices;
using FreelancerBlog.ViewModels.SiteOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FreelancerBlog.Controllers
{
    public class SiteOrderController : Controller
    {
        private IUnitOfWork _uw;
        private readonly IMapper _mapper;
        private IPriceSpecCollectionFactory<PriceSpec, object> _priceSpecCollectionFactory;
        private IFinalPriceCalculator<PriceSpec> _finalPriceCalculator;
        private ICaptchaValidator _captchaValidator;
        private IConfiguration _configuration;

        public SiteOrderController(IPriceSpecCollectionFactory<PriceSpec, object> priceSpecCollectionFactory, IFinalPriceCalculator<PriceSpec> finalPriceCalculator, IUnitOfWork uw, ICaptchaValidator captchaValidator, IConfiguration configuration)
        {
            _priceSpecCollectionFactory = priceSpecCollectionFactory;
            _finalPriceCalculator = finalPriceCalculator;
            _uw = uw;
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

            var siteOrder = _mapper.Map<SiteOrderViewModel, SiteOrder>(viewModel);

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