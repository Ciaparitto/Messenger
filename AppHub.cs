using messager.Signal.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace messager
{
    public class AppHub : Hub
    {
        public async Task SendMessage(string message,string UserId)
        {
            
            //await Clients.All.SendAsync("ReciveNotification", message);
            await Clients.Groups(UserId).SendAsync("ReciveNotification", message);
            Console.WriteLine(message);
           
        }
        public async Task SendMessage2(string message, string UserId)
        {

            await Clients.All.SendAsync("ReciveNotification", message);
            //await Clients.Groups(UserId).SendAsync("ReciveNotification", message);
            Console.WriteLine(message);

        }
        public async Task JoinGroup(string UserId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId);
        }
    }
}