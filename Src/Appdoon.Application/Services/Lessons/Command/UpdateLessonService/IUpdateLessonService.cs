using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Lessons.Command.UpdateLessonService
{
    public class UpdateLessonDto
    {
        public string Title { get; set; }
        public IFormFile TopBannerPhoto { get; set; }
        public string Text { get; set; }
        public string PhotoFileName { get; set; }
    }
    public interface IUpdateLessonService : ITransientService
    {
        Task<ResultDto> Execute(UpdateLessonDto updateLessonDto, int lessonId);
    }

    public class UpdateLessonService : IUpdateLessonService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public UpdateLessonService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto> Execute(UpdateLessonDto updateLessonDto, int lessonId)
        {
            try
            {
                var lesson = _context.Lessons.Where(l => l.Id == lessonId).FirstOrDefault();

                lesson.TopBannerSrc = await _facadeFileHandler.UpdateFile(
                    updateLessonDto.Title,
                    updateLessonDto.PhotoFileName,
                    "lessons",
                    "image/jpg",
                    lesson.TopBannerSrc,
                    updateLessonDto.TopBannerPhoto
                    );

                lesson.UpdateTime = DateTime.Now;
                lesson.Title = updateLessonDto.Title;
                lesson.Text = updateLessonDto.Text;

                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مقاله بروزرسانی شد.",
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
