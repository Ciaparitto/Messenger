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

        public AccountController(IUserService UserService, UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context, IImageSaver ImageSaver)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Context = Context;
            _ImageSaver = ImageSaver;
            _UserService = UserService;

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

                var result = await _UserManager.CreateAsync(NewUser, UserData.Password);

                if (result.Succeeded)
                {
                    /*
					if (_Context.Users.Where(User => User.Email == UserData.EmailAdress).ToList().Count != 0)
					{
						//ViewBag.Error = $"Email {UserData.EmailAdress} is already taken"; this code msut be done before  creating user account
					}
					*/

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
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            if (ModelState.IsValid)
            {
                if ((ChangePasswordModel.NewPassword == ChangePasswordModel.ConfirmPassword))
                {
                    await _UserService.ChangePassword(ChangePasswordModel.UserId, ChangePasswordModel.RecoveryCode, ChangePasswordModel.NewPassword);
                }
            }
            return Redirect("/Login");
        }
    }
}
