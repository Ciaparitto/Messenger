using messager.Signal.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace messager
{
    public class AppHub : Hub
    {
        public async Task ToAll(string message)
        {
            //await Clients.All.ToAll(message);
            await Clients.All.SendAsync("ReciveNotification", message);
            Console.WriteLine(message);
           
        }
        public async Task JoinGroup(string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}