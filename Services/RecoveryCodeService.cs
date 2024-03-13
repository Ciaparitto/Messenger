using Messenger.Services.Interfaces;

namespace Messenger.Services
{
	public class RecoveryCodeService : IRecoveryCodeService
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public RecoveryCodeService(AppDbContext Context, IUserGetter UserGetter)
		{
			_Context = Context;
			_UserGetter = UserGetter;
		}
		public string GetRecoveryCode(string UserId)
		{
			var RecoveryCode = _Context.Users.Where(user => user.Id == UserId).FirstOrDefault().RecoveryCode;
			return RecoveryCode;
		}
		public async Task ChangeRecoveryCode(string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				var Code = GenerateRecoveryCode();
				User.RecoveryCode = Code;
				await _Context.SaveChangesAsync();
			}
		}
		private string GenerateRecoveryCode()
		{
			Guid Guid = Guid.NewGuid();
			string Code = Guid.ToString();
			Code = Code.Replace("-", "").Substring(0, 10);
			return Code;
		}
	}
}
