using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Roadmaps.Command.UpdateRoadmapService
{
    public class UpdateRoadmapDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoFileName { get; set; }
        public IFormFile RoadmapPhoto { get; set; }
        public List<string> CategoryNames { get; set; }
        public int? Price { get; set; }
    }
    public interface IUpdateRoadmapService : ITransientService
    {
        Task<ResultDto> Execute(UpdateRoadmapDto updateRoadmapDto, int id);
    }

    public class UpdateRoadmapService : IUpdateRoadmapService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;
        public UpdateRoadmapService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto> Execute(UpdateRoadmapDto updateRoadmapDto, int id)
        {
            try
            {
                var roadmap = _context.RoadMaps.Include(r => r.Categories).Where(r => r.Id == id).FirstOrDefault();

                List<Category> categories = new List<Category>();
                if (updateRoadmapDto.CategoryNames.Count != 0)
                {
                    foreach (var item in updateRoadmapDto.CategoryNames)
                    {
                        Category category = _context.Categories.Where(s => s.Name == item).FirstOrDefault();
                        categories.Add(category);
                    }
                }

                roadmap.ImageSrc = await _facadeFileHandler.UpdateFile(
                    updateRoadmapDto.Title,
                    updateRoadmapDto.PhotoFileName,
                    "roadmaps",
                    "image/jpg",
                    roadmap.ImageSrc,
                    updateRoadmapDto.RoadmapPhoto
                    );

                roadmap.UpdateTime = DateTime.Now;
                roadmap.Title = updateRoadmapDto.Title;
                roadmap.Description = updateRoadmapDto.Description;
                roadmap.Categories = categories;
                roadmap.Price = updateRoadmapDto.Price;

                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "رودمپ بروزرسانی شد.",
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
