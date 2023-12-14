using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.LandingPage.Query.GetTopNewRoadmapsService
{
    public class TopNewRoadmapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public bool HasNewSrc { get; set; } = false;
        public int RateCount { get; set; }
        public DateTime InsertTime { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class TopNewRoadmapsDto
    {
        public List<TopNewRoadmapDto> Roadmaps { get; set; }
        public int Count { get; set; }
    }

    public interface IGetTopNewRoadmapsService : ITransientService
    {
        public Task<ResultDto<TopNewRoadmapsDto>> Execute(int count);
    }

    public class GetTopNewRoadmapsService : IGetTopNewRoadmapsService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetTopNewRoadmapsService(IDatabaseContext databaseContext, IFacadeFileHandler facadeFileHandler)
        {
            _context = databaseContext;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<TopNewRoadmapsDto>> Execute(int count)
        {
            try
            {
                int rowCount = 0;
                var roadmaps = _context.RoadMaps
                    .Include(r => r.Categories)
                    .Include(r => r.Students)
                    .OrderByDescending(r => r.InsertTime)
                    .Select(r => new TopNewRoadmapDto()
                    {
                        Id = r.Id,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Title = r.Title,
                        Categories = r.Categories,
                        RateCount = r.RateCount,
                        InsertTime = r.InsertTime
                    })
                    .ToPaged(1, count, out rowCount)
                    .ToList();

                foreach (var roadmap in roadmaps)
                {
                    string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
                    roadmap.HasNewSrc = (url != roadmap.ImageSrc);
                    roadmap.ImageSrc = url;
                }

                TopNewRoadmapsDto topNewRoadmaps = new TopNewRoadmapsDto()
                {
                    Roadmaps = roadmaps,
                    Count = rowCount,
                };

                return new ResultDto<TopNewRoadmapsDto>()
                {
                    IsSuccess = true,
                    Message = "جدید ترین رودمپ ها ارسال شدند.",
                    Data = topNewRoadmaps
                };

            }
            catch (Exception e)
            {
                return new ResultDto<TopNewRoadmapsDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new TopNewRoadmapsDto()
                };
            }
        }
    }


}
