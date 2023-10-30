using Appdoon.Application.Services.Users.Command.LoginUserService;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JWTAuthenticationController : ControllerBase
	{
		private readonly ILoginUserService _loginUserService;

		public JWTAuthenticationController(ILoginUserService loginUserService)
		{
			_loginUserService = loginUserService;
		}

		[HttpPost]
		public async Task<JsonResult> Login([FromServices] IJWTProvider jwtProvider, LoginUserDto loginUserDto)
		{
			var userResult = await _loginUserService.Execute(loginUserDto);

			if(userResult.IsSuccess == true)
			{
				var token = jwtProvider.Generate(userResult.Data);

				var result = new ResultDto<string>()
				{
					Data = token,
					IsSuccess = true
				};

				return new JsonResult(result);
			}

			return new JsonResult(userResult);
		}
	}
}
