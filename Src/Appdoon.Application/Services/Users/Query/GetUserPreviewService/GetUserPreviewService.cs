using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Query.GetUserPreviewService
{
    public interface IGetUserPreviewService : ITransientService
    {
        Task<ResultDto<GetUserPreviewDto>> Execute(int id);
    }

    public class GetUserPreviewDto
    {
        public string Username;
        public string Role;
        public string ProfileImageSrc;
    }

    public class GetUserPreviewService : IGetUserPreviewService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetUserPreviewService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<GetUserPreviewDto>> Execute(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(u => u.Id == id)
                .Select(u => new GetUserPreviewDto()
                {
                    Username = u.Username,
                    ProfileImageSrc = u.ProfileImageSrc,
                    Role = RoleToPersian(u.Roles.FirstOrDefault().Name)
                }).FirstOrDefault();

                // I doubt on it
                if (user == null)
                {
                    return new ResultDto<GetUserPreviewDto>()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!",
                        Data = new(),
                    };
                }

                string url = await _facadeFileHandler.GetFileUrl("users", user.ProfileImageSrc);
                user.ProfileImageSrc = url;

                return new ResultDto<GetUserPreviewDto>()
                {
                    IsSuccess = true,
                    Message = "اطلاعات یوزر دریافت شد",
                    Data = user,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<GetUserPreviewDto>()
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
