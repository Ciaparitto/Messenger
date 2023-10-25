using messager.Signal.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace messager
{
    public class AppHub : Hub<IAppHub>
    {
        public async Task ToAll(string message,string creatorid,string revicerid)
        {
            await Clients.All.ToAll(message);
         
            Console.WriteLine(message);
           
        }
        public async Task JoinGroup(string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}