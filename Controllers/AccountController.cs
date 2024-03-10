using Messenger.models;
using Messenger.Models;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;


namespace Messenger.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<UserModel> _UserManager;
		private readonly SignInManager<UserModel> _SignInManager;
		private readonly AppDbContext _Context;
		private readonly IImageSaver _ImageSaver;

		public AccountController(UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context, IImageSaver ImageSaver)
		{
			_UserManager = UserManager;
			_SignInManager = SignInManager;
			_Context = Context;
			_ImageSaver = ImageSaver;

		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(Register UserData, IFormFile ProfileImage)
		{


			if (ModelState.IsValid)
			{
				var NewUser = new UserModel
				{
					Email = UserData.EmailAdress,
					UserName = UserData.UserName,
				};

				var result = await _UserManager.CreateAsync(NewUser, UserData.Password);
				if (result.Succeeded)
				{
					await _ImageSaver.SaveImage(ProfileImage, NewUser.Id);
					await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					Console.WriteLine($"error error error error error {result.Errors.FirstOrDefault().Description}");
				}
				return View();
			}

			return View();
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(Login UserData)
		{
			if (ModelState.IsValid)
			{
				await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			await _SignInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}


	}
}
