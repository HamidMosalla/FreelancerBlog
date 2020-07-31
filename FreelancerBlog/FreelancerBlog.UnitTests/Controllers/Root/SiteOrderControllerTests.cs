using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Queries.Services.SiteOrder;
using FreelancerBlog.Core.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FreelancerBlog.UnitTests.Extensions;
using FreelancerBlog.Web.Controllers;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class SiteOrderControllerTests
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private SiteOrderController _sut;


        public SiteOrderControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _sut = new SiteOrderController(_mediator, _mapper);
        }

        [Fact]
        public async Task IndexPost_CaptchaValidationResultFalse_ReturnsFailedTheCaptchaValidation()
        {
            var captchaResponse = new CaptchaResponse { Success = "false" };

            A.CallTo(() => _mediator.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);

            var result = await _sut.Index(null);

            result.Should().NotBeNull();
            result.GetValueForProperty<string>("status")
                  .Should()
                  .Be("FailedTheCaptchaValidation");
        }

        [Fact]
        public async Task IndexPost_ModelStateNotValid_ReturnsFormWasNotValidJson()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };

            A.CallTo(() => _mediator.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);

            _sut.AddModelStateError();
            var result = await _sut.Index(null);

            result.GetValueForProperty<string>("Status")
                  .Should().Be("FormWasNotValid");
        }

        [Fact]
        public async Task IndexPost_MustAlways_ReturnTheCorrectType()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };

            A.CallTo(() => _mediator.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);
            A.CallTo(()=> _mediator.Send(A< PriceSpecCollectionQuery>._, A<CancellationToken>._)).Returns(new List<PriceSpec>());
            var result = await _sut.Index(null);

            result.GetValueForProperty<decimal>("Price").Should().BeOfType(typeof(decimal));
            result.GetValueForProperty<List<PriceSpec>>("PriceSheet").Should().BeOfType(typeof(List<PriceSpec>));
            result.GetValueForProperty<string>("Status").Should().Be("Success");
        }
    }
}
