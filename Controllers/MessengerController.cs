using messager.Signal.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using messager;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult pickuser()
		{
			var users = _Context.Users.ToList();
			return View(users);
		}
		[HttpPost]
		public IActionResult pickuser(string pickeduserid)
		{
			return RedirectToAction("Chat", "messenger", new { reciverid = pickeduserid });
		}
	}
}
