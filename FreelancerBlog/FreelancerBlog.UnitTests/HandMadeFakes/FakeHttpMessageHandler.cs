using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancerBlog.UnitTests.HandMadeFakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("Remember to setup this method with your mocking framework");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}