using System.Collections.Generic;

namespace FreelancerBlog.Core.Services.SiteOrderServices
{
    public interface IPriceSpecCollectionFactory<T1, T2>
    {
        List<T1> BuildPriceSpecCollection(T2 viewModel);
    }
}