using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Core.Services.SiteOrderServices
{
    public interface IFinalPriceCalculator<T>
    {

        decimal CalculateFinalPrice(List<T> priceSpecCollection);

    }
}
