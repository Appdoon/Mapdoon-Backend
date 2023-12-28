using Appdoon.Application.Services.Users.Command.LoginUserService;
using Appdoon.Application.Services.Users.Command.RegisterUserService;
using Appdoon.Application.Services.Users.Query.GetUserFromCookieService;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Mapdoon.Common.Interfaces;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromServices] IJWTProvider jwtProvider, LoginUserDto loginUserDto)
        {
            var userResult = await _loginUserService.Execute(loginUserDto);

            if (userResult.IsSuccess == true)
            {
                var token = jwtProvider.Generate(userResult.Data);

                var result = new ResultDto<string>()
                {
                    Data = token,
                    IsSuccess = true
                };

                return Ok(result);
            }

            return BadRequest(userResult);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromServices] IRegisterUserService registerUserService, [FromServices] IJWTProvider jwtProvider, RequestRegisterUserDto user)
        {
            // use new regiser user service
            var registerResult = await registerUserService.Execute(user);

            if (registerResult.IsSuccess == true)
            {
                var token = jwtProvider.Generate(new UserLoginInfoDto()
                {
                    Id = registerResult.Data.Id,
                    Email = registerResult.Data.Email,
                    Username = registerResult.Data.Username,
                });

                var result = new ResultDto<string>()
                {
                    Data = token,
                    IsSuccess = true
                };

                return Ok(result);
            }

            return BadRequest(registerResult);
        }

        [HttpGet("UserInfo")]
        public async Task<JsonResult> InfoFromCookie([FromServices] ICurrentContext currentContext, [FromServices] IGetUserFromCookieService getUserFromCookieService)
        {
            //TODO: Implement Global Error Handling
            try
            {
                int Id = currentContext.User.Id;
                var result = await getUserFromCookieService.Execute(Id);
                return new JsonResult(result);
            }
            catch { }

            return new JsonResult(new ResultDto()
            {
                IsSuccess = false,
                Message = "کاربر لاگین نکرده است!!"
            });
        }
    }
}
