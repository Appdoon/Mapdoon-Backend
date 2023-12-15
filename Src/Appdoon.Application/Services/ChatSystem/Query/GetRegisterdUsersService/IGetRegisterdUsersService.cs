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
    public interface IGetRegisterdUsersService : ITransientService
    {
        ResultDto<List<RegisterdUsersDto>> Execute(int roadmapId);
    }
    public class GetRegisterdUsersService : IGetRegisterdUsersService
    {
        private readonly IDatabaseContext _context;
        public GetRegisterdUsersService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RegisterdUsersDto>> Execute(int roadmapId)
        {
            try
            {
                var roadmap = _context.RoadMaps
                                      .AsNoTracking()
                                      .Include(rm => rm.Students)
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

                var teacherUser = _context.Users.FirstOrDefault(u => u.Id == roadmap.CreatoreId);

                var teacher = new RegisterdUsersDto
                {
                    Id = teacherUser.Id,
                    Firstname = teacherUser.FirstName,
                    Lastname = teacherUser.LastName,
                    Username = teacherUser.Username,
                    Role = "Teacher"
                };

                users.Add(teacher);

                return new ResultDto<List<RegisterdUsersDto>>()
                {
                    IsSuccess = true,
                    Message = "کاربران ارسال شدند",
                    Data = users,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<List<RegisterdUsersDto>>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new()
                };
            }
        }
    }

}
