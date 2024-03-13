﻿using Messenger.models;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Messenger.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeService _RecoveryCodeGetter;
		private readonly UserManager<UserModel> _UserManager;
		public UserService(IUserGetter UserGetter, AppDbContext Context, IRecoveryCodeService RecoveryCodeGetter, UserManager<UserModel> UserManager)
		{
			_UserGetter = UserGetter;
			_Context = Context;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_UserManager = UserManager;
		}
		public async Task ChangePassword(string Password, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{

			}
		}

	}
}
