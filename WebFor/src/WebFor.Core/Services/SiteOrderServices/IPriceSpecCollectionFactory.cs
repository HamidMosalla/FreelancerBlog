using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Core.Services.SiteOrderServices
{
    public interface IPriceSpecCollectionFactory<T1, T2>
    {

        List<T1> BuildPriceSpecCollection(T2 viewModel);

    }
}
