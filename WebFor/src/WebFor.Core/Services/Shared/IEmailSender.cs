using System.Threading.Tasks;

namespace WebFor.Core.Services.Shared
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
