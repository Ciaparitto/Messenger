using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace messager.Services
{
    public class SignalRManager
    {
        private Dictionary<string, string> connectionUserMap;
        private readonly IHubContext<AppHub> _HubContext;
        public HubConnection? hubConnection { get; private set; }
      
        public SignalRManager(IHubContext<AppHub> hubContext)
        {
            _HubContext = hubContext;

            var userId = "752fa934-1cac-470e-9586-12dfae758ad3";
           
            var url = $"https://localhost:7097/testhub?userId={userId}";
            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            hubConnection.StartAsync();
        }
        public Dictionary<string, string> GetConnectionUserMap()
        {
            return AppHub.ConnectionUserMap;
            
        }
       
    }
}