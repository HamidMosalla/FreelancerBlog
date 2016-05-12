using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebFor.Core;
using WebFor.Core.Services;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.SiteOrder;

namespace WebFor.Web.Controllers
{
    public class SiteOrderController : Controller
    {
        [FromServices]
        public IUnitOfWork  _db { get; set; }

        [FromServices]
        public IWebForMapper WebForMapper { get; set; }

        private IPriceSpecCollectionFactory<PriceSpec, object> _priceSpecCollectionFactory;
        private IFinalPriceCalculator<PriceSpec> _finalPriceCalculator;

        public SiteOrderController(IPriceSpecCollectionFactory<PriceSpec, object> priceSpecCollectionFactory, IFinalPriceCalculator<PriceSpec> finalPriceCalculator)
        {
            _priceSpecCollectionFactory = priceSpecCollectionFactory;
            _finalPriceCalculator = finalPriceCalculator;
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
            if (!ModelState.IsValid)
            {
                return Json(new {Status = "FormWasNotValid"});
            }

            var priceSpecCollection = _priceSpecCollectionFactory.BuildPriceSpecCollection(viewModel);

            var finalPrice = _finalPriceCalculator.CalculateFinalPrice(priceSpecCollection);

            var siteOrder = WebForMapper.SiteOrderViewModelToSiteOrder(viewModel);

            int addSiteOrderAsyncResult = await _db.SiteOrderRepository.AddSiteOrderAsync(siteOrder);

            if (addSiteOrderAsyncResult > 0)
            {
                return Json(new { Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Success" });
            }

            return Json(new {Price = finalPrice, PriceSheet = priceSpecCollection, Status = "Failed"});
        }
    }
}
