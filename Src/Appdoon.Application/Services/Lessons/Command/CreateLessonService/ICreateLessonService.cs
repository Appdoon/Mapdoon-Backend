using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Lessons.Command.CreateLessonService
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile TopBannerPhoto { get; set; }
        public string Text { get; set; } = string.Empty;
        public string PhotoFileName { get; set; }
    }
    public interface ICreateLessonService : ITransientService
    {
        Task<ResultDto> Execute(CreateLessonDto createLessonDto, int userId);
    }

    public class CreateLessonService : ICreateLessonService
    {
        private readonly IFacadeFileHandler _facadeFileHandler;
        private readonly IDatabaseContext _context;

        public CreateLessonService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _facadeFileHandler = facadeFileHandler;
            _context = context;
        }
        public async Task<ResultDto> Execute(CreateLessonDto createLessonDto, int userId)
        {
            try
            {
                var creator = _context.Users.Find(userId);
                if (creator == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر سازنده مقاله یافت نشد!",
                    };
                }

                string imageSrc = await _facadeFileHandler.CreateFile(
                    createLessonDto.Title,
                    createLessonDto.PhotoFileName, 
                    "lessons", "image/jpg", 
                    createLessonDto.TopBannerPhoto
                    );

                var lesson = new Lesson()
                {
                    Title = createLessonDto.Title,
                    Text = createLessonDto.Text,
                    TopBannerSrc = imageSrc,
                    Creator = creator,
                };

                _context.Lessons.Add(lesson);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مقاله ساخته شد",
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
