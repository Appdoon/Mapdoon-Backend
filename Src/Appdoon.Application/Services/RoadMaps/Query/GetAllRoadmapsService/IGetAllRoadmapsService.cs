using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Roadmaps.Query.GetAllRoadmapsService
{
    public class RoadMapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public int RateCount;
        public List<Category> Categories { get; set; }
    }
    public class AllRoadmapsDto
    {
        public List<RoadMapDto> Roadmaps { get; set; }
        public int RowCount { get; set; }
    }
    public interface IGetAllRoadmapsService : ITransientService
    {
        ResultDto<AllRoadmapsDto> Execute(int page_number, int page_size);
    }

    public class GetAllRoadmapsService : IGetAllRoadmapsService
    {
        private readonly IDatabaseContext _context;

        public GetAllRoadmapsService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<AllRoadmapsDto> Execute(int PageNumber, int PageSize)
        {
            try
            {
                int rowCount = 0;

                var roadmaps = _context.RoadMaps
                    .Include(r => r.Categories)
                    .Select(r => new RoadMapDto()
                    {
                        Id = r.Id,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Title = r.Title,
                        Categories = r.Categories,
                        RateCount = r.RateCount,

                    })
                    .ToPaged(PageNumber, PageSize, out rowCount)
                    .ToList();

                AllRoadmapsDto allRoadmapsDto = new AllRoadmapsDto()
                {
                    Roadmaps = roadmaps,
                    RowCount = rowCount,
                };

                return new ResultDto<AllRoadmapsDto>()
                {
                    IsSuccess = true,
                    Message = "رودمپ ها ارسال شد",
                    Data = allRoadmapsDto,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllRoadmapsDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new AllRoadmapsDto(),
                };
            }
        }
    }


}
