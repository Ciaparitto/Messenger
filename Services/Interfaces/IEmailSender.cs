namespace Messenger.Services.Interfaces
{
	public interface IEmailSender
	{
		Task SendEmail(string Email, string Subject, string Contnet, string UserId);
	}
}
