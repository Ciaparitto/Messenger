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
        public static  Dictionary<string, string> ConnectionUserMap = new Dictionary<string, string>();

        private readonly AppDbContext _Context;
        private readonly IUserService _UserService;
       
        public AppHub(AppDbContext context,IUserService userService)
        {
            _Context = context;
            _UserService = userService;
          
            
        }
       
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var userId = ConnectionUserMap[connectionId];
           
            /*
            if (!string.IsNullOrWhiteSpace(userId) && userId != null)
            {

                var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                user.IsOnline = false;
                await _Context.SaveChangesAsync();
                Console.WriteLine("zmiana statusu flase");
            }

            */
            ConnectionUserMap.Remove(connectionId);
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine(ConnectionUserMap.Count);
            Console.WriteLine("koniec polaczenia");
        }

        
        public override async Task OnConnectedAsync()
        {

            var userId = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();
            var connectionId = Context.ConnectionId;
            ConnectionUserMap[connectionId] = userId;
            Console.WriteLine(ConnectionUserMap.Count);
            /*
           if (!string.IsNullOrWhiteSpace(userId) && userId != null)
           {

               var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
               user.IsOnline = true;
               await _Context.SaveChangesAsync();
               Console.WriteLine("zmiana statusu true");
           }

           */


            await base.OnConnectedAsync();

            Console.WriteLine("Start polaczenia");
        }
        public Dictionary<string, string> GetConnectionUserMap()
        {
            return ConnectionUserMap;
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