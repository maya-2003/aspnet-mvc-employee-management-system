using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCS3.DAL.Models.IdentityModels;
using MVCS3PL.Utilities;
using MVCS3PL.ViewModels.IdentityViewModels;

namespace MVCS3PL.Controllers
{
	public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
	{

		#region Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var userToAdd = new ApplicationUser()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				UserName = model.UserName,
				Email = model.Email,
			};
			var res = _userManager.CreateAsync(userToAdd, model.Password).Result;
			if (res.Succeeded) return RedirectToAction("Login");
			else
			{
				foreach (var error in res.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);

				}
			}
			return View(model);
		}
		#endregion


		#region Login
		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public IActionResult LogIn(LoginViewModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var user = _userManager.FindByEmailAsync(model.Email).Result;
			if (user is not null)
			{
				var isCorrectPass = _userManager.CheckPasswordAsync(user, model.Password).Result;
				if (isCorrectPass)
				{
					var res = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
					if (res.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed ");
					if (res.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account Is Locked ");
					if (res.Succeeded)
						return RedirectToAction(nameof(HomeController.Index), "Home");
				}
			}
			else ModelState.AddModelError(string.Empty, "Invalid Login");
			return View();
		}
		#endregion

		[HttpGet]
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync();
			return RedirectToAction(nameof(LogIn));
		}

        [HttpGet]

		public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]

		public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
			if (ModelState.IsValid)
			{
				var user = _userManager.FindByEmailAsync(model.Email).Result;
				if (user is not null)
				{
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("Reset PasswordLink", "Account", new { email = model.Email , token}, Request.Scheme);
                    var mail = new Email()

					{ 
						To =model.Email,
						Subject= "Reset Password",
						Body= resetPasswordLink //ToDo

					};
					var res= EmailSettings.SendEmail(mail);
					if(res) return RedirectToAction("Check Your Inbox");
                }
			}
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), model);
        }

		[HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

		[HttpGet]
        public IActionResult ResetPasswordLink(string email, string token)
		{
			TempData["email"]=email;
			TempData["token"]=token;
			return View();

		}

		[HttpPost]
        public IActionResult ResetPasswordLink(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid) return View(model);
            var email =TempData["email"] as string;
            var token =TempData["token"] as string;
            var user=_userManager. FindByEmailAsync(email).Result;
            if (user is not null)
            {

                var res =_userManager.ResetPasswordAsync(user, token, model.Password).Result;
				if (res.Succeeded) return RedirectToAction(nameof(LogIn));
				else
				{
                    foreach (var error in res.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
                    
            }
			return View(model);

        }
    }
}
