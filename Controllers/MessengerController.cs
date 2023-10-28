using messager.Signal.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using messager;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace messager.Controllers
{

	public class MessengerController : Controller
	{
		private readonly AppDbContext _Context;
		public MessengerController(AppDbContext context)
		{
			_Context = context;
		}
		[HttpGet]
		[Authorize]
		public IActionResult pickuser()
		{			
				var users = _Context.Users.ToList();
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
