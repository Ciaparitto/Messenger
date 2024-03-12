namespace Messenger.Services.Interfaces
{
	public interface IUserService
	{
		Task ChangeRecoveryCode(string UserId);
	}
}
