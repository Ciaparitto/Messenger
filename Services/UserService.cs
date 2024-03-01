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
        private readonly IMessageService _MessageService;
        private readonly AppDbContext _Context;
        public UserService(IMessageService MessageService, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor,AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _MessageService = MessageService;
            _Context = context;

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
            if (_User != null)
            {
                return _User;
            }
            return null;
        }
        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }    
        public async Task<List<UserModel>> GetUsers(string CreatorId)
        {              
            var MessageList = await _MessageService.GetMessagesByCreator(CreatorId);
            MessageList = MessageList.Concat(await _MessageService.GetMessagesByReceiver(CreatorId)).ToList();
            var GroupedMessageList = MessageList.GroupBy(msg => msg.CreatorId);
            GroupedMessageList = GroupedMessageList.Concat(MessageList.GroupBy(msg => msg.ReciverId));
			var OrderedGroupedMessageList = GroupedMessageList.OrderBy(group => group.Max(msg => msg.SendTime));
           

            var Ids = OrderedGroupedMessageList.Select(group => group.Key).ToList();
            Ids.Reverse();
            var UserList = new List<UserModel>();
            foreach(var id in  Ids)
            {
                if(!(id == CreatorId) && !UserList.Contains(await GetUserById(id)))
                {
                    UserList.Add(await GetUserById(id));
                }
            

            }       
            return UserList;                           
        }
    }
}
