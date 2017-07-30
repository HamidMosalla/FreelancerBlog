using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Types;
using MediatR;

namespace FreelancerBlog.Core.Queries.Services.Shared
{
    public class ValidateCaptchaQuery : IRequest<CaptchaResponse> {  }
}