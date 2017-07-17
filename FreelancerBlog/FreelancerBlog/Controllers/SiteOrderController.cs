using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Commands.SiteOrders;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Services.SiteOrderServices;
using FreelancerBlog.ViewModels.SiteOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FreelancerBlog.Controllers
{
    public class SiteOrderController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IPriceSpecCollectionFactory<PriceSpec, object> _priceSpecCollectionFactory;
        private IFinalPriceCalculator<PriceSpec> _finalPriceCalculator;
        private ICaptchaValidator _captchaValidator;
        private IConfiguration _configuration;

        public SiteOrderController(IPriceSpecCollectionFactory<PriceSpec, object> priceSpecCollectionFactory, IFinalPriceCalculator<PriceSpec> finalPriceCalculator, ICaptchaValidator captchaValidator, IConfiguration configuration, IMediator mediator)
        {
            _priceSpecCollectionFactory = priceSpecCollectionFactory;
            _finalPriceCalculator = finalPriceCalculator;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
            _mediator = mediator;
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
                return Json(new { Status = "FormWasNotValid" });
            }

            var priceSpecCollection = _priceSpecCollectionFactory.BuildPriceSpecCollection(viewModel);

            var finalPrice = _finalPriceCalculator.CalculateFinalPrice(priceSpecCollection);

            var siteOrder = _mapper.Map<SiteOrderViewModel, SiteOrder>(viewModel);

            await _mediator.Send(new AddSiteOrderCommand { SiteOrder = siteOrder });

            return Json(new { Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Success" });
        }
    }
}