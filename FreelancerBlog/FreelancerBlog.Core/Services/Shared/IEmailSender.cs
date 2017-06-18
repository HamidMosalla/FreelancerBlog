using System.Threading.Tasks;

namespace FreelancerBlog.Core.Services.Shared
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
