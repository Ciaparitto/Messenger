using messager.models;
using messager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace messager.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<UserModel> _UserManager;
		private readonly SignInManager<UserModel> _SignInManager;
		private readonly AppDbContext _Context;

		public AccountController(UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context)
		{
			_UserManager = UserManager;
			_SignInManager = SignInManager;
			_Context = Context;

		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(Register UserData, List<IFormFile> Files)
		{


			if (ModelState.IsValid)
			{
				var NewUser = new UserModel
				{
					Email = UserData.EmailAdress,
					UserName = UserData.UserName,
				};
				IdentityResult result = await _UserManager.CreateAsync(NewUser, UserData.Password);

				if (result.Succeeded)
				{

					if (Files != null && Files.Count > 0)
					{
						foreach (var ImageFile in HttpContext.Request.Form.Files)
						{

							using var memoryStream = new MemoryStream();

							ImageFile.CopyTo(memoryStream);

							var Image = new Image
							{
								image = memoryStream.ToArray(),
								ContentType = ImageFile.ContentType,
								UserId = NewUser.Id

							};
							_Context.ImageList.Add(Image);
							_Context.SaveChanges();
							NewUser.ProfileImageId = Image.Id;

							_Context.SaveChanges();
						}
					}
					else
					{
						NewUser.ProfileImageId = 1;
						_Context.SaveChanges();
					}
				}
				await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);

				return RedirectToAction("Index", "Home");
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
