
using Messenger.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Messenger.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly IUserGetter _UserGetter;
		public EmailSender(IUserGetter userGetter)
		{
			_UserGetter = userGetter;
		}
		public async Task SendEmail(string Email, string Subject, string Contnet, string UserId)
		{
			var Mail = "Test2Mail@gmail.com";
			var MailPassword = "Password";

			var MailClient = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(Mail, MailPassword)
			};

			await MailClient.SendMailAsync(
			   new MailMessage(
				   from: Mail,
				   to: Email,
				   Subject,
				   Contnet
				   ));
		}
	}
}
