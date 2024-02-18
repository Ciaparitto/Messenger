using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace messager.Services
{
    public class SignalRManager
    {
        public HubConnection? hubConnection { get; private set; }

        public SignalRManager()
        {
            var userId = "SA";
            hubConnection = new HubConnectionBuilder()
           .WithUrl($"/testhub?userId={userId}") // NAPRAW TEN LINK BO NIE DZIALA
           .Build();

            hubConnection.StartAsync();


        }
    }
}