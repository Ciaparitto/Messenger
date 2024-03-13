namespace Messenger.Services.Interfaces
{
	public interface IUserService
	{
		public Task ChangePassword(string Password, string UserId);
	}
}
