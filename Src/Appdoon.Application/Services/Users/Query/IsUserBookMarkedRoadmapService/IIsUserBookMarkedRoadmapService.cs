using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Query.IsUserBookMarkedRoadmapService
{
    public interface IIsUserBookMarkedRoadmapService : ITransientService
    {
        ResultDto<bool> Execute(int roadmapId, int userId);
    }

    public class IsUserBookMarkedRoadmapService : IIsUserBookMarkedRoadmapService
    {
        private readonly IDatabaseContext _context;

        public IsUserBookMarkedRoadmapService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<bool> Execute(int roadmapId, int userId)
        {
            try
            {
                var user = _context.Users
                    .Select(u => new User()
                    { 
                        Id = u.Id,
                        BookmarkedRoadMaps = u.BookmarkedRoadMaps
                        .Select(b => new RoadMap() { Id = b.Id })
                        .ToList(),
                    })
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if (user == null)
                {
                    return new ResultDto<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "این کاربر وجود ندازد!",
                    };
                }

                var userBookmarkedRoadmapsId = user.BookmarkedRoadMaps
                    .Select(b => b.Id)
                    .ToList();

                bool result = userBookmarkedRoadmapsId.Any(u => u == roadmapId);

                return new ResultDto<bool>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = "کاربر این رودمپ را بوک مارک کرده است.",
                };
            }
            catch (Exception e)
            {
                return new ResultDto<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }
}
