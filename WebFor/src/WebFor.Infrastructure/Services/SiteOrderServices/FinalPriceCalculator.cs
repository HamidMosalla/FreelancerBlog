using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;

namespace WebFor.Infrastructure.Services.SiteOrderServices
{
    public class FinalPriceCalculator : IFinalPriceCalculator<PriceSpec>
    {
        public decimal CalculateFinalPrice(List<PriceSpec> priceSpecCollection)
        {
            var intTypes = priceSpecCollection.SingleOrDefault(n => n.Value.GetType().Name == "Int32");
            var stringTypes = priceSpecCollection.Where(n => n.Value.GetType().Name == "String").ToList();
            var boolTypes = priceSpecCollection.Where(n => n.Value.GetType().Name == "Boolean").ToList();

            decimal TotalPrice;

            #region Calculate Bool Prices
            var boolTotalPrice = boolTypes.Sum(b => b.Price);

            TotalPrice = boolTotalPrice;
            #endregion

            #region Calculate Int Prices
            if (intTypes != null)
            {
                var intTotalPrice = int.Parse(intTypes.Value.ToString()) * intTypes.Price;

                TotalPrice += intTotalPrice;
            }
            #endregion

            #region Calculate String Prices
            var siteOrderNumberOfMockUp = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderNumberOfMockUp");

            if (siteOrderNumberOfMockUp != null)
            {
                //SiteOrderNumberOfMockUp
                //1
                //2
                //3
                //5

                var siteOrderNumberOfMockUpPrice =
                    (string)siteOrderNumberOfMockUp.Value == "1"
                        ? 1 * siteOrderNumberOfMockUp.Price
                        : (string)siteOrderNumberOfMockUp.Value == "2"
                            ? 2 * siteOrderNumberOfMockUp.Price
                            : (string)siteOrderNumberOfMockUp.Value == "3"
                                ? 3 * siteOrderNumberOfMockUp.Price
                                : (string)siteOrderNumberOfMockUp.Value == "5" ? 5 * siteOrderNumberOfMockUp.Price : 0;

                TotalPrice += siteOrderNumberOfMockUpPrice;
            }

            var siteOrderWebSiteType = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderWebSiteType");

            if (siteOrderWebSiteType != null)
            {
                //SiteOrderWebSiteType
                //Personal
                //Blog
                //Shop

                var siteOrderWebSiteTypePrice =
                    (string)siteOrderWebSiteType.Value == "Personal"
                        ? 20000
                        : (string)siteOrderWebSiteType.Value == "Blog"
                            ? 40000
                            : (string)siteOrderWebSiteType.Value == "Shop" ? 70000 : 0;

                TotalPrice += siteOrderWebSiteTypePrice;
            }

            var siteOrderDevelopmentComplexity = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderDevelopmentComplexity");

            if (siteOrderDevelopmentComplexity != null)
            {
                //SiteOrderDevelopmentComplexity
                //Standard
                //Advanced
                //Enterprise

                var siteOrderDevelopmentComplexityPrice =
                    (string)siteOrderDevelopmentComplexity.Value == "Standard"
                        ? 20000
                        : (string)siteOrderDevelopmentComplexity.Value == "Advanced"
                            ? 40000
                            : (string)siteOrderDevelopmentComplexity.Value == "Enterprise" ? 70000 : 0;

                TotalPrice += siteOrderDevelopmentComplexityPrice;
            }

            var siteOrderSupportType = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderSupportType");

            if (siteOrderSupportType != null)
            {
                //SiteOrderSupportType
                //NoSupport
                //OneYear
                //TwoYear
                //FiveYear
                //IndefiniteSupport

                var siteOrderSupportTypePrice =
                    (string)siteOrderSupportType.Value == "NoSupport"
                        ? 20000
                        : (string)siteOrderSupportType.Value == "OneYear"
                            ? 40000
                            : (string)siteOrderSupportType.Value == "TwoYear"
                                ? 80000
                                : (string)siteOrderSupportType.Value == "FiveYear"
                                    ? 160000
                                    : (string)siteOrderSupportType.Value == "IndefiniteSupport" ? 320000 : 0;

                TotalPrice += siteOrderSupportTypePrice;
            }

            var siteOrderSeoType = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderSeoType");

            if (siteOrderSeoType != null)
            {
                //SiteOrderSeoType
                //Standard
                //Advanced
                //Enterprise

                var siteOrderSeoTypePrice =
                    (string)siteOrderSeoType.Value == "Standard"
                        ? 20000
                        : (string)siteOrderSeoType.Value == "Advanced"
                            ? 40000
                            : (string)siteOrderSeoType.Value == "Enterprise" ? 70000 : 0;

                TotalPrice += siteOrderSeoTypePrice;
            }

            var siteOrderDoesUseAjaxType = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderDoesUseAjaxType");

            if (siteOrderDoesUseAjaxType != null)
            {
                //SiteOrderDoesUseAjaxType
                //Standard
                //Advanced
                //Enterprise

                var siteOrderDoesUseAjaxTypePrice =
                    (string)siteOrderDoesUseAjaxType.Value == "Standard"
                        ? 20000
                        : (string)siteOrderDoesUseAjaxType.Value == "Advanced"
                            ? 40000
                            : (string)siteOrderDoesUseAjaxType.Value == "Enterprise" ? 70000 : 0;

                TotalPrice += siteOrderDoesUseAjaxTypePrice;
            }

            var siteOrderSearchType = stringTypes.SingleOrDefault(s => s.EnName == "SiteOrderSearchType");

            if (siteOrderSearchType != null)
            {
                //SiteOrderSearchType
                //Standard
                //Advanced
                //Enterprise

                var siteOrderSearchTypePrice =
                    (string)siteOrderSearchType.Value == "Standard"
                        ? 20000
                        : (string)siteOrderSearchType.Value == "Advanced"
                            ? 40000
                            : (string)siteOrderSearchType.Value == "Enterprise" ? 70000 : 0;

                TotalPrice += siteOrderSearchTypePrice;
            }
            #endregion

            return TotalPrice;
        }
    }
}
