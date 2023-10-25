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
        public float Question1 {get;set;} 
        public float Question2 {get;set;}
        public float Question3 {get;set;}
        public float Question4 {get;set;}
        public float Question5 {get;set;}
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
                    Question1 = createRate.Question1,
                    Question2 = createRate.Question2,
                    Question3 = createRate.Question3,
                    Question4 = createRate.Question4,
                    Question5 = createRate.Question5,
                    UserId = userId,
                    RoadMapId = roadmapId,
                };
                _context.Rates.Add(rate);
                var RoadMap = _context.RoadMaps
                .Where(r => r.Id == roadmapId).First();
                RoadMap.Stars = (rate.Question1 + rate.Question2 + rate.Question3 + rate.Question4 + rate.Question5) / 5;
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
                    Message = "خطا در ثبت امتیاز!",
                };
            }
        }

    }
}
