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

        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _scopeFactory;
        
        public UserService(IServiceScopeFactory scopeFactory,UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _scopeFactory = scopeFactory;


        }
        public async Task Register(string username, string password, string Email)
        {
            var NewUser = new UserModel
            {
                Email = Email,
                UserName = username,
            };
            await _userManager.CreateAsync(NewUser, password);
            await _signInManager.PasswordSignInAsync(username, password, false, false);
        }
        public async Task Login(string username, string password)
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
        public async Task<List<UserModel>> GetUsersByIds(IEnumerable<string> userIds)
        {
            return await _userManager.Users.Where(user => userIds.Contains(user.Id)).ToListAsync();
        }

        public async Task<List<UserModel>> GetUsers(string CreatorId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var Context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

                var MessageList = await messageService.GetMessages(CreatorId);
                var _User = GetLoggedUser();
                HashSet<string> UserIds = new HashSet<string>(MessageList.Select(msg => msg.ReciverId));
                var UserList = await GetUsersByIds(UserIds);
                return UserList;
            }
            
           
        }
    }
}
