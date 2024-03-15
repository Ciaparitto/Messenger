
using Messenger.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Messenger.Services
{
    public class EmailSender : IEmailSender
    {

        public Task SendEmail(string Email, string RecoveryCode)
        {
            var Mail = "RecoveryCodeMessenger@outlook.com";
            var MailPassword = "aBc987654321!134";

            var MailClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(Mail, MailPassword)
            };
            var Subject = "Password recovery code";
            var Content = $"Your password recovery code: {RecoveryCode}";

            return MailClient.SendMailAsync(
               new MailMessage(
                   from: Mail,
                   to: Email,
                   Subject,
                   Content
                   ));
        }
    }
}
