using messager.Signal.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace messager
{
    public class AppHub : Hub<IAppHub>
    {
        public async Task ToAll(string message)
        {
            await Clients.All.ToAll(message);
            Console.WriteLine(message);
           
        }
    }
}