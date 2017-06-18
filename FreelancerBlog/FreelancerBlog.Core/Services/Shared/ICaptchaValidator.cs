using System.Threading.Tasks;
using FreelancerBlog.Core.Types;

namespace FreelancerBlog.Core.Services.Shared
{
    public interface ICaptchaValidator
    {
        Task<CaptchaResponse> ValidateCaptchaAsync(string secret);
    }
}
