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

namespace Appdoon.Application.Services.LandingPage.Query.GetTopEnrolledRoadmapsService
{
    public class TopEnrolledRoadmapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public bool HasNewSrc { get; set; } = false;
        public int RateCount { get; set; }
        public int StudentCount { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class TopEnrolledRoadmapsDto
    {
        public List<TopEnrolledRoadmapDto> Roadmaps { get; set; }
        public int Count { get; set; }
    }
    public interface IGetTopEnrolledRoadmapsService : ITransientService
    {
        public Task<ResultDto<TopEnrolledRoadmapsDto>> Execute(int count);
    }

    public class GetTopEnrolledRoadmapsService : IGetTopEnrolledRoadmapsService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetTopEnrolledRoadmapsService(IDatabaseContext databaseContext, IFacadeFileHandler facadeFileHandler)
        {
            _context = databaseContext;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<TopEnrolledRoadmapsDto>> Execute(int count)
        {
            try
            {
                int rowCount = 0;
                var roadmaps = _context.RoadMaps
                    .Include(r => r.Categories)
                    .Include(r => r.Students)
                    .OrderByDescending(r => r.Students.Count)
                    .Select(r => new TopEnrolledRoadmapDto()
                    {
                        Id = r.Id,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Title = r.Title,
                        Categories = r.Categories,
                        RateCount = r.RateCount,
                        StudentCount = r.Students.Count
                    })
                    .ToPaged(1, count, out rowCount)
                    .ToList();

                foreach (var roadmap in roadmaps)
                {
                    string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
                    roadmap.HasNewSrc = (url != roadmap.ImageSrc);
                    roadmap.ImageSrc = url;
                }

                TopEnrolledRoadmapsDto topEnrolledRoadmaps = new TopEnrolledRoadmapsDto()
                {
                    Roadmaps = roadmaps,
                    Count = rowCount,
                };

                return new ResultDto<TopEnrolledRoadmapsDto>()
                {
                    IsSuccess = true,
                    Message = "رودمپ های داری بیشترین دانش آموز ارسال شدند.",
                    Data = topEnrolledRoadmaps
                };

            }
            catch (Exception e)
            {
                return new ResultDto<TopEnrolledRoadmapsDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new TopEnrolledRoadmapsDto()
                };
            }
        }
    }


}
