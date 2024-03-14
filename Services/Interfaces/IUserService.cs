namespace Messenger.Services.Interfaces
{
    public interface IUserService
    {
        public Task ChangePassword(string UserId, string Token, string Password);
    }
}
