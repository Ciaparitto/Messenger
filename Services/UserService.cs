﻿using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace messager.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessageService _messageService;
        private readonly AppDbContext _Context;
        public UserService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor, IMessageService messageService, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _messageService = messageService;
            _Context = context;

        }
        public async void Register(string username, string password, string Email)
        {
            var NewUser = new UserModel
            {
                Email = Email,
                UserName = username,
            };
            await _userManager.CreateAsync(NewUser, password);
            await _signInManager.PasswordSignInAsync(username, password, false, false);
        }
        public async void Login(string username, string password)
        {
            await _signInManager.PasswordSignInAsync(username, password, false, false);
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<UserModel> GetLoggedUser()
        {
            var _User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return _User;
        }
        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<List<UserModel>> GetUsersByIds(IEnumerable<string> userIds)
        {

            return await _userManager.Users.Where(user => userIds.Contains(user.Id)).ToListAsync();

        }

        public async Task<List<UserModel>> GetUsers(string CreatorId)
        {

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("DefaultConnection")
                .Options;
            using (var context = new AppDbContext(options))
            {
                var MessageList = await _messageService.GetMessages(CreatorId);
                var _User = GetLoggedUser();
                HashSet<string> UserIds = new HashSet<string>(MessageList.Select(msg => msg.ReciverId));
                var UserList = await GetUsersByIds(UserIds);
                return UserList;


            }

        }
    }
}
