using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.RoadMaps.Query.GetPreviewRoadmapService
{
    public interface IGetPreviewRoadmapService : ITransientService
    {
        /// <summary>
        /// Return preview of roadmap with only 3 steps
        /// </summary>
        /// <param name="RoadmapId"></param>
        /// <returns></returns>
        Task<ResultDto<PreviewRoadmapDto>> Execute(int RoadmapId);
    }

    public class GetPreviewRoadmapService : IGetPreviewRoadmapService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetPreviewRoadmapService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto<PreviewRoadmapDto>> Execute(int RoadmapId)
        {
            try
            {
                var preveiwRoadmap = _context.RoadMaps
                    .Include(r => r.Categories)
                    .Include(r => r.Steps.Take(3))
                        .ThenInclude(s => s.ChildSteps)
                    .Where(r => r.Id == RoadmapId)
                    .Select(r => new PreviewRoadmapDto()
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Categories = r.Categories,
                        Price = r.Price,
                        Steps = r.Steps.Take(3).ToList(),
                       
                    })
                    .FirstOrDefault();

                if (preveiwRoadmap == null)
                {
                    return new ResultDto<PreviewRoadmapDto>()
                    {
                        IsSuccess = false,
                        Message = "رودمپ یافت نشد!",
                        Data = new(),
                    };
                }

                string url = await _facadeFileHandler.GetFileUrl("roadmaps", preveiwRoadmap.ImageSrc);
                preveiwRoadmap.HasNewSrc = (url != preveiwRoadmap.ImageSrc);
                preveiwRoadmap.ImageSrc = url;

                return new ResultDto<PreviewRoadmapDto>()
                {
                    IsSuccess = true,
                    Data = preveiwRoadmap,
                    Message = "پیش نمایش رودمپ با موفقیت ارسال شد!",
                };
            }
            catch (Exception e)
            {
                return new ResultDto<PreviewRoadmapDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }

    public class PreviewRoadmapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public List<Category> Categories { get; set; }
        public bool HasNewSrc { get; set; } = false;
        public List<Step> Steps { get; set; }
        public float? Price { get; set; }
    }
}
