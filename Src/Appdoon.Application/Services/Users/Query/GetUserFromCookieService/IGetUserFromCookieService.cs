using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Query.GetUserFromCookieService
{
    public interface IGetUserFromCookieService : ITransientService
    {
        Task<ResultDto<UserCookieDto>> Execute(int id);
    }

    public class GetUserFromCookieService : IGetUserFromCookieService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetUserFromCookieService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto<UserCookieDto>> Execute(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(u => u.Id == id)
                    .Include(u => u.Roles).FirstOrDefault();

                // I doubt on it
                if (user == null)
                {
                    return new ResultDto<UserCookieDto>()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!",
                        Data = new(),
                    };
                }

                var userCookie = new UserCookieDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Roles[0].Name

                };

                string url = await _facadeFileHandler.GetFileUrl("users", user.ProfileImageSrc);
                userCookie.ProfileImageSrc = url;

                return new ResultDto<UserCookieDto>()
                {
                    IsSuccess = true,
                    Message = "اطلاعات یوزر دریافت شد",
                    Data = userCookie,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<UserCookieDto>()
                {
                    Data = new(),
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
    public class UserCookieDto
    {
        public int? Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ProfileImageSrc { get; set; }
    }
}
