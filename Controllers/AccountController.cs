using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
	public class AccountController : Controller
	{
	private UserManager<IdentityUser> _userManager;
	private SignInManager<IdentityUser> _signInManager;
	public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[AllowAnonymous]
		public ViewResult Login(string returnUrl)
		{
			return View(new LoginModel { 
				ReturnUrl= returnUrl
			});
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await _userManager.FindByNameAsync(model.Name);
				if (user is not null)
				{
					await _signInManager.SignOutAsync();
					if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
					{
						return Redirect(model?.ReturnUrl ?? "/Admin/index");
					}
				}
			}
			ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
			return View(model);
		}
		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
