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
		private readonly IUserService _UserService;
		private readonly UserManager<UserModel> _UserManager;
		private readonly SignInManager<UserModel> _SignInManager;
		private readonly AppDbContext _Context;
		private readonly IImageSaver _ImageSaver;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		private readonly IRecoveryCodeGenerator _RecoveryCodeGenerator;
		private readonly IEmailSender _EmailSender;

		public AccountController(IEmailSender EmailSender, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter, IUserService UserService, UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context, IImageSaver ImageSaver, IUserGetter UserGetter)
		{
			_UserManager = UserManager;
			_SignInManager = SignInManager;
			_Context = Context;
			_ImageSaver = ImageSaver;
			_UserService = UserService;
			_UserGetter = UserGetter;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_RecoveryCodeGenerator = RecoveryCodeGenerator;
			_EmailSender = EmailSender;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel UserData, IFormFile? ProfileImage)
		{


			if (ModelState.IsValid)
			{
				var NewUser = new UserModel
				{
					Email = UserData.EmailAdress,
					UserName = UserData.UserName,
				};
				if (_Context.Users.Where(User => User.Email == UserData.EmailAdress).ToList().Count != 0)
				{
					ViewBag.Error = $"Email {UserData.EmailAdress} is already taken";
					return View();
				}

				var result = await _UserManager.CreateAsync(NewUser, UserData.Password);

				if (result.Succeeded)
				{


					if (ProfileImage != null && ProfileImage.Length > 0)
					{
						await _ImageSaver.SaveImage(ProfileImage, NewUser.Id);
					}


					await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);

					return RedirectToAction("Index", "Home");


				}
				else
				{

					ViewBag.Error = result.Errors.FirstOrDefault().Description;
				}

			}

			return View();
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel UserData)
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
		[HttpGet]
		public IActionResult ChangePassword(string Email)
		{
			var ChangePasswordModel = new ChangePasswordModel
			{
				Email = Email
			};
			return View(ChangePasswordModel);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordModel ChangePasswordModel)
		{
			if (ModelState.IsValid)
			{
				if ((ChangePasswordModel.NewPassword == ChangePasswordModel.ConfirmPassword))
				{
					var User = await _UserGetter.GetUserByEmail(ChangePasswordModel.Email);

					var UserRecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);

					if (UserRecoveryCode == ChangePasswordModel.RecoveryCode)
					{
						await _UserService.ChangePassword(User, ChangePasswordModel.NewPassword);
						return Redirect("/");
					}
				}
			}
			return View(ChangePasswordModel);
		}
		[HttpGet]
		public IActionResult GetRecoveryCode()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GetRecoveryCode(EmailModel EmailModel)
		{
			if (ModelState.IsValid)
			{
				var User = await _UserGetter.GetUserByEmail(EmailModel.Email);
				await _RecoveryCodeGenerator.ChangeRecoveryCode(User.Id);
				var RecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);
				await _EmailSender.SendEmail(EmailModel.Email, RecoveryCode);
				return RedirectToAction("ChangePassword", "Account", new { Email = EmailModel.Email });
			}
			return View(EmailModel);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePasswordByPassword(string CurrentPassword, string NewPassword, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				if (await _UserManager.CheckPasswordAsync(User, CurrentPassword))
				{
					var Result = await _UserManager.ChangePasswordAsync(User, CurrentPassword, NewPassword);
					if (Result.Succeeded)
					{
						await _Context.SaveChangesAsync();
					
					}
				}
			}
			return Redirect("/");

		}
		[HttpPost]
		public async Task<IActionResult> ChangeUsername(string CurrentPassword, string NewUsername, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				if (await _UserManager.CheckPasswordAsync(User, CurrentPassword))
				{
					var Result = await _UserManager.SetUserNameAsync(User, NewUsername);
					if (Result.Succeeded)
					{
						await _Context.SaveChangesAsync();
					}

				}
			}
			return Redirect("/");
		}
	}

}

