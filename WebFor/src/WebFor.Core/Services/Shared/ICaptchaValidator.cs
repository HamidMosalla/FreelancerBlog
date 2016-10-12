using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Types;

namespace WebFor.Core.Services.Shared
{
    public interface ICaptchaValidator
    {
        Task<CaptchaResponse> ValidateCaptchaAsync(string secret);
    }
}
