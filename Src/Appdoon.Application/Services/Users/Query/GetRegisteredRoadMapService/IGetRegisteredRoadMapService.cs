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

namespace Appdoon.Application.Services.Users.Query.GetRegisteredRoadMapService
{
    public interface IGetRegisteredRoadMapService : ITransientService
    {
        Task<ResultDto<List<RegisteredRoadMapDto>>> Execute(int id);
    }
    public class RegisteredRoadMapDto
    {
        public int Id;
        public string Title;
        public string ImageSrc;
        public bool HasNewSrc = false;
    }
    public class GetRegisteredRoadMapService : IGetRegisteredRoadMapService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetRegisteredRoadMapService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto<List<RegisteredRoadMapDto>>> Execute(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(r => r.Id == id)
                    .Include(r => r.SignedRoadMaps)
                    .FirstOrDefault();

                if (user == null)
                {
                    return new ResultDto<List<RegisteredRoadMapDto>>()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!",
                        Data = new(),
                    };
                }

                user.SignedRoadMaps ??= new List<RoadMap>();

                var roadmaps = user.SignedRoadMaps
                    .Select(r => new RegisteredRoadMapDto()
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

                return new ResultDto<List<RegisteredRoadMapDto>>()
                {
                    IsSuccess = true,
                    Message = "رودمپ های ثبت نام شده ی یوزر دریافت شد",
                    Data = roadmaps,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<List<RegisteredRoadMapDto>>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new(),
                };
            }
        }
    }
}
