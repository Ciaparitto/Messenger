using messager.Signal.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using messager.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Documents;
using messager.Services.Interfaces;
using messager.Services;
using System.Net.Http;

using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace messager
{
    public class AppHub :Hub
    {
        private readonly AppDbContext _Context;
        private readonly IUserService _UserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppHub> _logger;
        public AppHub(AppDbContext context,IUserService userService, IHttpContextAccessor httpContextAccessor, ILogger<AppHub> logger)
        {
            _Context = context;
            _UserService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;

            Console.WriteLine(userId);
          
            if (userId != null)
            {

                var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                user.IsOnline = false;
                await _Context.SaveChangesAsync();
                Console.WriteLine("zmiana statusu false");
            }

            await base.OnDisconnectedAsync(exception);

            Console.WriteLine("koniec polaczenia");
        }

        
        public override async Task OnConnectedAsync()
        {

            var userId = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();
             
            if(!string.IsNullOrWhiteSpace(userId) && userId != null)
            {
           
                var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                user.IsOnline = true;
                await _Context.SaveChangesAsync();
                Console.WriteLine("zmiana statusu true");
            }
             
            
           

            await base.OnConnectedAsync();

            Console.WriteLine("Start polaczenia");
        }
        public async Task<UserModel> GetUser()
        {
            var user = Context.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                _logger.LogInformation($"Użytkownik {user.Identity.Name} jest uwierzytelniony.");
            }
            else
            {
                _logger.LogInformation("Brak uwierzytelnionego użytkownika.");
            }

            var user1 = await _UserService.GetLoggedUser(_Context);
         
            return user1;
        }
        public async Task SendMessage(string message,string UserId)
        {
            
          
            await Clients.Groups(UserId).SendAsync("ReciveNotification", message);
         
           
        }
        public async Task SendToAll(UserModel User)
        {

            await Clients.Groups("GlobalGroup").SendAsync("ReciveUserData", User);

        }
        public async Task JoinGroup(string UserId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId);
            
        }
    
    
    }
}