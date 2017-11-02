using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.Commands.Data.SiteOrders;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Queries.Services.SiteOrder;
using FreelancerBlog.Core.Types;
using FreelancerBlog.ViewModels.SiteOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class SiteOrderController : Controller
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public SiteOrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

            var priceSpecCollection = await _mediator.Send(new PriceSpecCollectionQuery {ViewModel = viewModel});

            var finalPrice = _mediator.Send(new FinalPriceQuery {PriceSpecs = priceSpecCollection});

            var siteOrder = _mapper.Map<SiteOrderViewModel, SiteOrder>(viewModel);

            await _mediator.Send(new AddSiteOrderCommand { SiteOrder = siteOrder });

            return Json(new { Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Success" });
        }
    }
}