using messager.Signal.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using messager;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using messager.models;

namespace messager.Controllers
{

	public class MessengerController : Controller
	{
		private readonly UserManager<UserModel> _userManager;
		private readonly AppDbContext _Context;
		public MessengerController(AppDbContext context, UserManager<UserModel> userManager)
		{
			_Context = context;
			_userManager = userManager;
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
	}
}
