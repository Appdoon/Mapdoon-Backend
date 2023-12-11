using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Mapdoon.Application.Services.ChatSystem.Query.GetAllMessagesService;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.ChatSystem.Query.GetRegisterdUsersService
{
    public class RegisterdUsersDto
    { 
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Role {  get; set; }
    }
    public class AllRegisterdUsersDto
    {
        public List<RegisterdUsersDto> RegisteredUsers { get; set; }
    }
    public interface IGetRegisterdUsersService : ITransientService
    {
        ResultDto<AllRegisterdUsersDto> Execute(int roadmapId);
    }
    public class GetRegisterdUsersService : IGetRegisterdUsersService
    {
        private readonly IDatabaseContext _context;
        public GetRegisterdUsersService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<AllRegisterdUsersDto> Execute(int roadmapId)
        {
            try
            {
                var roadmap = _context.RoadMaps
                    .FirstOrDefault(rm => rm.Id == roadmapId);
                var users = roadmap.Students
                    .Select(s => new RegisterdUsersDto
                    {
                        Id = s.Id,
                        Firstname = s.FirstName,
                        Lastname = s.LastName,
                        Username = s.Username,
                        Role = "User"
                    })
                    .ToList();
                var t = _context.Users.FirstOrDefault(u => u.Id == roadmap.CreatoreId);
                var teacher = new RegisterdUsersDto
                {
                    Id = t.Id,
                    Firstname = t.FirstName,
                    Lastname = t.LastName,
                    Username = t.Username,
                    Role = "Teacher"
                };

                users.Add(teacher);

                AllRegisterdUsersDto allregistereduseres = new AllRegisterdUsersDto();
                allregistereduseres.RegisteredUsers = users;

                return new ResultDto<AllRegisterdUsersDto>()
                {
                    IsSuccess = true,
                    Message = "کاربران ارسال شدند",
                    Data = allregistereduseres
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllRegisterdUsersDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new AllRegisterdUsersDto()
                };
            }
        }
    }

}
