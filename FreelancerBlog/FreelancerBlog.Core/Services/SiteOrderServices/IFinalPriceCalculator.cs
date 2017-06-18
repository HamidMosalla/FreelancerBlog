using System.Collections.Generic;

namespace FreelancerBlog.Core.Services.SiteOrderServices
{
    public interface IFinalPriceCalculator<T>
    {

        decimal CalculateFinalPrice(List<T> priceSpecCollection);

    }
}
