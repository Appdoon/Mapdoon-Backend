using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Query.GetUserService
{
    public interface IGetUserService : ITransientService
    {
        Task<ResultDto<GetUserDto>> Execute(int id);
    }

    public class GetUserDto
    {
        public string Username;
        public string FirstName;
        public string LastName;
        public string Email;
        public string PhoneNumber;
        public string Role;
        public string ProfileImageSrc;
    }

    public class GetUserService : IGetUserService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetUserService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<GetUserDto>> Execute(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(u => u.Id == id)
                .Select(u => new GetUserDto()
                {
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ProfileImageSrc = u.ProfileImageSrc,
                    Role = RoleToPersian(u.Roles.FirstOrDefault().Name)
                }).FirstOrDefault();

                // I doubt on it
                if (user == null)
                {
                    return new ResultDto<GetUserDto>()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!",
                        Data = new(),
                    };
                }

                string url = await _facadeFileHandler.GetFileUrl("users", user.ProfileImageSrc);
                user.ProfileImageSrc = url;

                return new ResultDto<GetUserDto>()
                {
                    IsSuccess = true,
                    Message = "اطلاعات یوزر دریافت شد",
                    Data = user,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<GetUserDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new(),
                };
            }
        }

        static string RoleToPersian(string role)
        {
            switch (role)
            {
                case "User":
                    return "دانش آموز";
                case "Teacher":
                    return "معلم";
                case "Admin":
                    return "مدیر سایت";
                default:
                    return "دانش آموز";
            }
        }

    }

}
