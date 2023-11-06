using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace messager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _Context;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessageService _messageService;
		public UserService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _httpContextAccessor = httpContextAccessor;
            _Context = context;
            
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
        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<List<UserModel>> GetUsers(string CreatorId)
        {
           
                var MessageList = _Context.MessageList.Where(x => x.CreatorId == CreatorId).ToList();

                var _User = GetLoggedUser();
                List<UserModel> UserList = new List<UserModel>();
                List<UserModel> UserList2 = new List<UserModel>();
                foreach (var message in MessageList)
                {
                    if (UserList.Count == 0)
                    {
                        var reciver = await GetUserById(message.ReciverId);
                        UserList.Add(reciver);
                        UserList2.Add(reciver);
                    }
                    else
                    {
                        if (UserList.Count != 0 && _User != null)
                        {
                            foreach (var user in UserList)
                            {
                                if (message.ReciverId != user.Id)
                                {

                                    var reciver = await GetUserById(message.ReciverId);
                                    UserList2.Add(reciver);
                                }
                            }
                        }
                    }
                }

                var EndUserList = UserList2.Distinct().ToList();
                return EndUserList;
            

            
        }

    }
}
