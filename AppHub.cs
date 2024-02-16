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
namespace messager
{
    public class AppHub :Hub
    {
        private readonly AppDbContext _Context;
        private readonly IUserService _UserService;
        public AppHub(AppDbContext context,IUserService userService)
        {
            _Context = context;
            _UserService = userService;
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await GetUser();
            if (user != null)
            {
                user.IsOnline = false;
                await _Context.SaveChangesAsync();
                Console.WriteLine("zmiana statusu true");
            }

            await base.OnDisconnectedAsync(exception);

            Console.WriteLine("koniec polaczenia");
        }

      
        public override async Task OnConnectedAsync()
        {


            var user = await GetUser();
            if(user != null)
            {
                user.IsOnline = true;
                await _Context.SaveChangesAsync();
                Console.WriteLine("zmiana statusu true");
            }
             
            
           

            await base.OnConnectedAsync();

            Console.WriteLine("Start polaczenia");
        }
        public async Task<UserModel> GetUser()
        {
            //zrob zeby poprawnie pobieralo usera
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("/controller_name/GetLoggedUser");


            if (response.IsSuccessStatusCode)
            {

                string responseBody = await response.Content.ReadAsStringAsync();


                var user = JsonSerializer.Deserialize<UserModel>(responseBody);

                Console.WriteLine(user.UserName);
                return user;
            }
            return null;
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