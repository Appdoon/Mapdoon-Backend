using Appdoon.Application.Interfaces;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Common.Pagination;
using Mapdoon.Common.Interfaces;
using Mapdoon.Application.Interfaces;

namespace Appdoon.Application.Services.RoadMaps.Query.SearchRoadmapsService
{
    public class RoadMapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public bool HasNewSrc { get; set; } = false;
        public List<Category> Categories { get; set; }
        public float? Price { get; set; }
    }
    public class AllRoadmapsDto
    {
        public List<RoadMapDto> Roadmaps { get; set; }
        public int RowCount { get; set; }
    }
    public interface ISearchRoadmapsService : ITransientService
    {
        Task<ResultDto<AllRoadmapsDto>> Execute(string searched_text, int page_number, int page_size);
    }
    public class SearchRoadmapsService : ISearchRoadmapsService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;
        public SearchRoadmapsService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<AllRoadmapsDto>> Execute(string searched_text, int page_number, int page_size)
        {
            try
            {
                int rowCount = 0;
                var roadmaps = _context.RoadMaps
                    .Where(r => r.Title.Contains(searched_text))
                    .Include(r => r.Categories)
                    .Select(r => new RoadMapDto()
                    {
                        Id = r.Id,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Title = r.Title,
                        Categories = r.Categories,
                        Price = r.Price
                    }).ToPaged(page_number, page_size, out rowCount)
                    .ToList();

                foreach (var roadmap in roadmaps)
                {
                    string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
                    roadmap.HasNewSrc = (url != roadmap.ImageSrc);
                    roadmap.ImageSrc = url;
                }

                AllRoadmapsDto allRoadmapsDto = new AllRoadmapsDto();
                allRoadmapsDto.Roadmaps = roadmaps;
                allRoadmapsDto.RowCount = rowCount;

                return new ResultDto<AllRoadmapsDto>()
                {
                    IsSuccess = true,
                    Message = "رودمپ ها پیدا و ارسال شد",
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
