using System.Threading.Tasks;
using FreelancerBlog.Core.Services.Shared;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FreelancerBlog.Services.Shared
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
            //var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(_authMessageSenderSecrets.Email);
            //mailMessage.To.Add(new MailAddress(email));
            //mailMessage.Subject = subject;
            //mailMessage.Body = message;
            //mailMessage.IsBodyHtml = true;

            //var smtp = new SmtpClient();

            //var credential = new NetworkCredential
            //{
            //    UserName = _authMessageSenderSecrets.Email,
            //    Password = _authMessageSenderSecrets.Password
            //};

            //smtp.Credentials = credential;
            //smtp.Host = "mail.server.com";
            //smtp.Port = 25;
            //return smtp.SendMailAsync(mailMessage);

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("FreelancerBlog", _authMessageSenderSecrets.Email));
            mailMessage.To.Add(new MailboxAddress("Client", email));
            mailMessage.Subject = subject;


            var bodyBuilder = new BodyBuilder { HtmlBody = message };
            mailMessage.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("mail.address.com", 25, false);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            //remove all of it except "LOGIN" because server doesn't support falling back to other method if one fail
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.AuthenticationMechanisms.Remove("CRAM-MD5");
            client.AuthenticationMechanisms.Remove("NTLM");
            //var supportedMechanisms = client.AuthenticationMechanisms;

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(_authMessageSenderSecrets.Email, _authMessageSenderSecrets.Password);

            return client.SendAsync(mailMessage);
            //client.Disconnect(true);

        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
