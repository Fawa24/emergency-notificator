using Core.Domain.Entities;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identify_demo.Web.Controllers
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterDTO registerDTO)
		{
			if (!ModelState.IsValid)
			{
				List<string> validationErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
				return BadRequest(validationErrors);
			}

			ApplicationUser user = new ApplicationUser()
			{
				UserName = registerDTO.UserName,
				FullName = registerDTO.FullName,
				Email = registerDTO.Email,
				PhoneNumber = registerDTO.PhoneNumber,
			};

			IdentityResult result = await _userManager.CreateAsync(user, password: registerDTO.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
				return Ok("Registration is succesfull");
			}

			foreach (IdentityError error in result.Errors)
			{
				ModelState.AddModelError("Register", error.Description);
			}

			List<string> errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
			return BadRequest(errors);
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				List<string> validationErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
				return BadRequest(validationErrors);
			}

			var result = await _signInManager.PasswordSignInAsync(
				loginDTO.UserName,
				password: loginDTO.Password,
				isPersistent: false,
				lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return Ok("Signed in succesfully");
			}

			return BadRequest("Invalid username or password");
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<ActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("Signed out succesfully");
		}

		[HttpGet("current")]
		public ActionResult CurrentUser()
		{
			if (User.Identity.IsAuthenticated)
			{
				return Ok(User.Identity.Name);
			}
			return Ok("Not auntificated");
		}
	}
}
