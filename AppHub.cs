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
        public static Dictionary<string, string> ConnectionUserMap = new Dictionary<string, string>();
             
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;     
            ConnectionUserMap.Remove(connectionId);
            await base.OnDisconnectedAsync(exception);         
        }
        public override async Task OnConnectedAsync()
        {          
            var UserId = Context.GetHttpContext().Request.Headers["UserId"].ToString();
            var connectionId = Context.ConnectionId;                            
            ConnectionUserMap[connectionId] = UserId;        
            await base.OnConnectedAsync();      
        }
        public Dictionary<string, string> GetConnectionUserMap()
        {
            return ConnectionUserMap;
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId.ToString();
        }
        public async Task SendMessage(string message,string UserId)
        {  
            await Clients.Groups(UserId).SendAsync("ReciveNotification", message);
                  
        }    
        public async Task JoinGroup(string UserId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId);
            
        }
    
    
    }
}