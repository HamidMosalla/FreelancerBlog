using System.Collections.Generic;
using System.Linq;
using FreelancerBlog.Core.Services.SiteOrderServices;

namespace FreelancerBlog.Infrastructure.Services.SiteOrderServices
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
                        ? 1500000
                        : (string)siteOrderWebSiteType.Value == "Blog"
                            ? 1500000
                            : (string)siteOrderWebSiteType.Value == "Shop" ? 2500000 : 0;

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
                        ? 0
                        : (string)siteOrderDevelopmentComplexity.Value == "Advanced"
                            ? 1000000
                            : (string)siteOrderDevelopmentComplexity.Value == "Enterprise" ? 2000000 : 0;

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
                        ? 400000
                        : (string)siteOrderSupportType.Value == "OneYear"
                            ? 600000
                            : (string)siteOrderSupportType.Value == "TwoYear"
                                ? 80000
                                : (string)siteOrderSupportType.Value == "FiveYear"
                                    ? 1200000 : 0;

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
                        ? 300000
                        : (string)siteOrderSeoType.Value == "Advanced"
                            ? 800000
                            : (string)siteOrderSeoType.Value == "Enterprise" ? 1500000 : 0;

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
                        ? 300000
                        : (string)siteOrderDoesUseAjaxType.Value == "Advanced"
                            ? 500000
                            : (string)siteOrderDoesUseAjaxType.Value == "Enterprise" ? 800000 : 0;

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
                        ? 0
                        : (string)siteOrderSearchType.Value == "Advanced"
                            ? 160000
                            : (string)siteOrderSearchType.Value == "Enterprise" ? 370000 : 0;

                TotalPrice += siteOrderSearchTypePrice;
            }
            #endregion

            if (boolTypes.SingleOrDefault(b => b.EnName == "SiteOrderDoesSourceCodeIncluded") != null)
            {
                TotalPrice = TotalPrice * 6;
            }

            return TotalPrice;
        }
    }
}
