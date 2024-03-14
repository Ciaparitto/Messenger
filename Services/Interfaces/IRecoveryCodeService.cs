namespace Messenger.Services.Interfaces
{
    public interface IRecoveryCodeService
    {
        public string GetRecoveryCode(string UserId);
        public Task<string> GenerateRecoveryCode(string UserId);

    }
}
