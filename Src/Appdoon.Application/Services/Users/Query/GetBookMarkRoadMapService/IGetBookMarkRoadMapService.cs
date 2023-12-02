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

namespace Appdoon.Application.Services.Users.Query.GetBookMarkRoadMapService
{
    public interface IGetBookMarkRoadMapService : ITransientService
    {
        Task<ResultDto<List<BookMarkRoadMapDto>>> Execute(int id);
    }
    public class BookMarkRoadMapDto
    {
        public int Id;
        public string Title;
        public string ImageSrc;
        public bool HasNewSrc = false;
    }
    public class GetBookMarkRoadMapService : IGetBookMarkRoadMapService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetBookMarkRoadMapService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto<List<BookMarkRoadMapDto>>> Execute(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(r => r.Id == id)
                    .Include(r => r.BookmarkedRoadMaps)
                    .FirstOrDefault();

                if (user == null)
                {
                    return new ResultDto<List<BookMarkRoadMapDto>>()
                    {
                        IsSuccess = false,
                        Message = "کابر یافت نشد!",
                        Data = new(),
                    };
                }

                user.BookmarkedRoadMaps ??= new List<RoadMap>();

                var roadmaps = user.BookmarkedRoadMaps
                    .Select(r => new BookMarkRoadMapDto()
                    {
                        Title = r.Title,
                        ImageSrc = r.ImageSrc,
                        Id = r.Id,
                    }).ToList();

                foreach (var roadmap in roadmaps)
                {
                    string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
                    roadmap.HasNewSrc = (url != roadmap.ImageSrc);
                    roadmap.ImageSrc = url;
                }

                return new ResultDto<List<BookMarkRoadMapDto>>()
                {
                    IsSuccess = true,
                    Message = "رودمپ های مورد علاقه ی یوزر دریافت شد",
                    Data = roadmaps,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<List<BookMarkRoadMapDto>>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new(),
                };
            }
        }
    }
}
