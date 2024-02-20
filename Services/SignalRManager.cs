using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http;

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
            var user = GetUser();
            var userId = user.Id;


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
        private async Task<UserModel> GetUser()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("/Account/GetLoggedUser");

            string responseBody = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<UserModel>(responseBody);

            return user;
        }
      
       
       
    }
}