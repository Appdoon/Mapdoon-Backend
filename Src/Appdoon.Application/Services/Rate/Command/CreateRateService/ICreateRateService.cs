using Appdoon.Application.Interfaces;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Common.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Rate.Command.CreateRateService
{
    public class CreateRateDto
    {
        public float Score {get;set;}
    }

    public interface ICreateRateService : ITransientService
    {
        ResultDto Execute(int roadmapId, int userId , CreateRateDto Rate);
    }

    public class CreateRateService : ICreateRateService
    {
        private readonly IDatabaseContext _context;
        public CreateRateService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int roadmapId , int userId , CreateRateDto createRate)
        {
            try
            {
                RateRoadMap rate = new RateRoadMap()
                {
                    Score = createRate.Score,
                    UserId = userId,
                    RoadMapId = roadmapId,
                };
                _context.Rates.Add(rate);
                var RoadMap = _context.RoadMaps
                .Where(r => r.Id == roadmapId).First();
                var sum = RoadMap.Stars * RoadMap.RateCount ;
                RoadMap.RateCount += 1;
                RoadMap.Stars = (rate.Score + sum )/ RoadMap.RateCount;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "امتیاز با موفقیت ثبت شد.",
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
}
