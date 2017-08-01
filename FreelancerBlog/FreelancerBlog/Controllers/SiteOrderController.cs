using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.Commands.Data.SiteOrders;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Services.SiteOrderServices;
using FreelancerBlog.ViewModels.SiteOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class SiteOrderController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IPriceSpecCollectionFactory<PriceSpec, object> _priceSpecCollectionFactory;
        private IFinalPriceCalculator<PriceSpec> _finalPriceCalculator;

        public SiteOrderController(IPriceSpecCollectionFactory<PriceSpec, object> priceSpecCollectionFactory, IFinalPriceCalculator<PriceSpec> finalPriceCalculator, IMediator mediator)
        {
            _priceSpecCollectionFactory = priceSpecCollectionFactory;
            _finalPriceCalculator = finalPriceCalculator;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Index(SiteOrderViewModel viewModel)
        {
            CaptchaResponse captchaResult = await _mediator.Send(new ValidateCaptchaQuery());

            if (captchaResult.Success != "true") return Json(new { status = "FailedTheCaptchaValidation" });

            if (!ModelState.IsValid) return Json(new { Status = "FormWasNotValid" });

            var priceSpecCollection = _priceSpecCollectionFactory.BuildPriceSpecCollection(viewModel);

            var finalPrice = _finalPriceCalculator.CalculateFinalPrice(priceSpecCollection);

            var siteOrder = _mapper.Map<SiteOrderViewModel, SiteOrder>(viewModel);

            await _mediator.Send(new AddSiteOrderCommand { SiteOrder = siteOrder });

            return Json(new { Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Success" });
        }
    }
}