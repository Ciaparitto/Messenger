using messager.models;
using messager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using System;

namespace messager.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly AppDbContext _Context;
        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(Register UserData, List<IFormFile> files)
        {

			Console.WriteLine("liczba zdjec" +files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			Console.WriteLine("liczba zdjec" + files.Count);
			if (ModelState.IsValid)
            {
                var NewUser = new UserModel
                {
                    Email = UserData.EmailAdress,
                    UserName = UserData.UserName,
                };
				await _userManager.CreateAsync(NewUser, UserData.Password);
              //zdjecia sie nie dodaja

                    if (files != null && files.Count > 0)
                    {

                        foreach (var imageFile in HttpContext.Request.Form.Files)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                imageFile.CopyTo(memoryStream);

                                var image = new Image
                                {
                                    image = memoryStream.ToArray(),
                                    ContentType = imageFile.ContentType,
                                    UserId = "7f67f35b-3fd6-417d-8169-59e6146d5f94"

                                };
                                _Context.ImageList.Add(image);


                            }
                        }
                    }
                
                else
                {
                    Console.WriteLine("nie dziala");
                }
				await _signInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
			
				return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Index()
        {
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
                await _signInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
