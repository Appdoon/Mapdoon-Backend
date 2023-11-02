using Appdoon.Application.Services.Users.Command.CheckUserResetPasswordLinkService;
using Appdoon.Application.Services.Users.Command.ForgetPasswordUserService;
using Appdoon.Application.Services.Users.Command.LoginUserService;
using Appdoon.Application.Services.Users.Command.RegisterUserService;
using Appdoon.Application.Services.Users.Command.ResetPasswordService;
using Appdoon.Application.Services.Users.Query.GetUserFromCookieService;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IRegisterUserService _registerUserService;
		private readonly ILoginUserService _loginUserService;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly ICheckUserResetPasswordLinkService _checkUserResetPasswordLinkService;
		private readonly IGetUserFromCookieService _getUserFromCookieService;
		private readonly ICurrentContext _currentContext;

		public AuthenticationController(IRegisterUserService registerUserService,
			ILoginUserService loginUserService,
			IResetPasswordService resetPasswordService,
			ICheckUserResetPasswordLinkService checkUserResetPasswordLinkService,
			IGetUserFromCookieService getUserFromCookieService,
			ICurrentContext currentContext)
		{
			_registerUserService = registerUserService;
			_loginUserService = loginUserService;
			_resetPasswordService = resetPasswordService;
			_checkUserResetPasswordLinkService = checkUserResetPasswordLinkService;
			_getUserFromCookieService = getUserFromCookieService;
			_currentContext = currentContext;
		}

		[HttpPost]
		public async Task<JsonResult> Login(LoginUserDto user)
		{
			var result = await _loginUserService.Execute(user);

			if(result.IsSuccess == true)
			{
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.NameIdentifier,result.Data.Id.ToString()),
					new Claim(ClaimTypes.Email,user.Email),
					new Claim(ClaimTypes.Name,result.Data.Username),
					new Claim(ClaimTypes.Role,UserRole.User.ToString()),
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// Remember me check box
				bool Remember = true;
				var properties = new AuthenticationProperties()
				{
					IsPersistent = Remember,
				};

				await HttpContext.SignInAsync(principal, properties);
			}
			return new JsonResult(result);
		}

		[HttpPost]
		public JsonResult Register(RequestRegisterUserDto user)
		{
			// use new regiser user service
			var result = _registerUserService.Execute(user);

			if(result.IsSuccess == true)
			{
				var claims = new List<Claim>()
				{
					// Set ID,Email,Name
					new Claim(ClaimTypes.NameIdentifier,result.Data.ToString()),
					new Claim(ClaimTypes.Email,user.Email),
					new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName),
					new Claim(ClaimTypes.Role,UserRole.User.ToString()),
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);
				var properties = new AuthenticationProperties()
				{
					IsPersistent = true,
				};

				HttpContext.SignInAsync(principal, properties);
			}

			return new JsonResult(result);
		}

		[HttpGet]
		public JsonResult UserSignOut()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return new JsonResult(new ResultDto()
			{
				IsSuccess = true,
				Message = "خروج موفق!",
			});
		}


		[HttpPost]
		public async Task<JsonResult> ForgetPassword([FromServices] IForgetPasswordUserService forgetPasswordUserService, ForgetPasswordEmailDto forgetPasswordEmailDto)
        {
			var result = await forgetPasswordUserService.Execute(forgetPasswordEmailDto);
			return new JsonResult(result);
        }

		[HttpPost]
		public async Task<JsonResult> ResetPassword(string password, string repeatPassword, int userId)
        {
			var result = await _resetPasswordService.Execute(password, repeatPassword, userId);
			return new JsonResult(result);
        }

		[HttpGet]
		public async Task<JsonResult> CheckResetPasswordLink(int userId, string token)
        {
			var result = await _checkUserResetPasswordLinkService.Execute(userId, token);
			return new JsonResult(result);
		}

		[HttpGet]
		public JsonResult InfoFromCookie()
		{
			int Id = _currentContext.User.Id;
			var result = _getUserFromCookieService.Execute(Id);
			return new JsonResult(result);
		}
	}
}
