using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Appdoon.Application.Services.LandingPage.Query.GetStatisticsService
{
    public class GetStatisticsDto
    {
        public int UserCount { get; set; }
        public int TeacherCount { get; set; }
        public int RoadmapCount { get; set; }
        public int HomeworkCount { get; set; }
    }
    public interface IGetStatisticsService : ITransientService
    {
        public ResultDto<GetStatisticsDto> Execute();
    }

    public class GetStatisticsService : IGetStatisticsService
    {
        private readonly IDatabaseContext _context;

        public GetStatisticsService(IDatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public ResultDto<GetStatisticsDto> Execute()
        {
            try
            {
                GetStatisticsDto getStatisticsDto = new GetStatisticsDto();
                getStatisticsDto.UserCount = _context.Users.Count();

                getStatisticsDto.TeacherCount = _context.Users
                    .Include(u => u.Roles)
                    .Where(u => u.Roles.Select(r => r.Name).Contains("Teacher"))
                    .Select(u => u.Roles.Select(r => r.Name))
                    .ToList()
                    .Count();

                getStatisticsDto.RoadmapCount = _context.RoadMaps.Count();
                getStatisticsDto.HomeworkCount = _context.Homeworks.Count();
                return new ResultDto<GetStatisticsDto>()
                {
                    IsSuccess = true,
                    Message = "آمار ارسال شد.",
                    Data = getStatisticsDto
                };

            }
            catch (Exception e)
            {
                return new ResultDto<GetStatisticsDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new GetStatisticsDto()
                };
            }
        }
    }


}
