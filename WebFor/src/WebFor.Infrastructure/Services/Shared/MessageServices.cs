using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private AuthMessageSenderSecrets _authMessageSenderSecrets;

        public AuthMessageSender(IOptions<AuthMessageSenderSecrets> authMessageSenderSecrets)
        {
            _authMessageSenderSecrets = authMessageSenderSecrets.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_authMessageSenderSecrets.Email);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            var smtp = new SmtpClient();
           

            var credential = new NetworkCredential
            {
                UserName = _authMessageSenderSecrets.Email,
                Password = _authMessageSenderSecrets.Password
            };

            smtp.Credentials = credential;
            smtp.Host = "mail.webfor.ir";
            smtp.Port = 25;
            return smtp.SendMailAsync(mailMessage);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
