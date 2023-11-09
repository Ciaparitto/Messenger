using messager.Signal.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using messager;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using messager.models;
using messager.Services.Interfaces;
using messager.Services;

namespace messager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessengerController : Controller
	{
		private readonly UserManager<UserModel> _userManager;
		private readonly AppDbContext _Context;
		public readonly IUserService _UserService;
		public MessengerController(IUserService userService,AppDbContext context, UserManager<UserModel> userManager)
		{
			_Context = context;
			_userManager = userManager;
			_UserService = userService;
		}
		[HttpGet]
		[Authorize]
		public IActionResult pickuser()
		{
			var USER = _userManager.GetUserAsync(User).Result;
			ViewBag.UserId = USER.Id;
			var users = _Context.Users.AsNoTracking().ToListAsync();
			return View(users);			
		}
		[HttpPost]
        [Authorize]
        public IActionResult pickuser(string pickeduserid)
		{
			return RedirectToAction("Chat", "messenger", new { reciverid = pickeduserid });
		}
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string creatorId)
        {
            var userList = await _UserService.GetUsers(creatorId);
            return Ok(userList); 
        }
    }
}
