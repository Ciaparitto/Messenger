using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace messager.Services
{
    public class UserService : IUserService
    {
     
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _httpContextAccessor = httpContextAccessor;
        }
        public async void Register(string username, string password,string Email) 
        {
            var NewUser = new UserModel
            {
                Email = Email,
                UserName = username,
            };
            await _userManager.CreateAsync(NewUser, password);
            await _signInManager.PasswordSignInAsync(username, password, false, false);
        }
        public async void Login(string username, string password)
        {
            await _signInManager.PasswordSignInAsync(username, password, false, false);
        }
        public async void Logout() 
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<UserModel> GetLoggedUser()
        {
			var _User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			return _User;
        }
    }
}
