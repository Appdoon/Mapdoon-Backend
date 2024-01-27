using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Users.Command.RegisterUserService;
using Appdoon.Application.Validatores.UserValidatore;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Command.EditUserService
{
    public interface IEditUserService : ITransientService
    {
        Task<ResultDto> Execute(int id, EditUserDto editUserDto);
    }

    public class EditUserService : IEditUserService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public EditUserService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto> Execute(int id, EditUserDto editUserDto)
        {
            try
            {
                UserValidatore validationRules = new UserValidatore();
                var result = validationRules.Validate(new RequestRegisterUserDto()
                {
                    Username = editUserDto.Username,
                    FirstName = editUserDto.FirstName,
                    LastName = editUserDto.LastName,
                    PhoneNumber = editUserDto.PhoneNumber,
                });

                List<string> properties = new List<string>()
                {
                    "Username",
                    "FirstName",
                    "LastName",
                    "PhoneNumber",
                };

                var errors = result.Errors.Where(e => properties.Contains(e.PropertyName))
                    .Select(e => e.ErrorMessage)
                    .ToList();

                if (errors.Count != 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = errors.First(),
                    };
                }

                var dupUsername = _context.Users.FirstOrDefault(u => u.Username == editUserDto.Username);
                if (dupUsername != null && dupUsername.Id != id)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام کاربری تکراری است!",
                    };
                }

                var user = _context.Users.Find(id);

                user.ProfileImageSrc = await _facadeFileHandler.UpdateFile(
                    editUserDto.Username,
                    editUserDto.PhotoFileName,
                    "users",
                    "image/jpg",
                    user.ProfileImageSrc,
                    editUserDto.ProfilePhoto
                    );

                user.Username = editUserDto.Username;
                user.FirstName = editUserDto.FirstName;
                user.LastName = editUserDto.LastName;
                user.PhoneNumber = editUserDto.PhoneNumber;

                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت تغییر یافت!",
                };
            }
            catch (Exception e)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }

    public class EditUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoFileName { get; set; } = string.Empty;
        public IFormFile ProfilePhoto { get; set; } = null;
    }
}
