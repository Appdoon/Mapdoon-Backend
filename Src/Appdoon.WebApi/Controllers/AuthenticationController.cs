using Appdoon.Application.Services.Users.Command.ForgetPasswordUserService;
using Appdoon.Application.Services.Users.Command.ResetPasswordService;
using Appdoon.Common.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IResetPasswordService _resetPasswordService;

        public AuthenticationController(IResetPasswordService resetPasswordService)
        {
            _resetPasswordService = resetPasswordService;
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
        public async Task<JsonResult> ResetPassword(string password, string repeatPassword, int userId, string token)
        {
            var result = await _resetPasswordService.Execute(password, repeatPassword, userId, token);
            return new JsonResult(result);
        }
    }
}
