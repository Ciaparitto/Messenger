using Messenger.Services.Interfaces;

namespace Messenger.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public UserService(IUserGetter UserGetter, AppDbContext Context)
		{
			_UserGetter = UserGetter;
			_Context = Context;
		}
		public async Task ChangeRecoveryCode(string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				Guid Guid = Guid.NewGuid();
				string Code = Guid.ToString();
				Code = Code.Replace("-", "").Substring(0, 10);

				User.RecoveryCode = Code;
				await _Context.SaveChangesAsync();
			}
		}
	}
}
