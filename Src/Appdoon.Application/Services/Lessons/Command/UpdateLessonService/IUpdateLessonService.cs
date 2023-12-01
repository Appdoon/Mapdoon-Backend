using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Lessons.Command.CreateLessonService;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.HashFunctions;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
		private readonly IFileHandler _fileHandler;

        public UpdateLessonService(IDatabaseContext context, IFileHandler fileHandler)
		{
			_context = context;
			_fileHandler = fileHandler;
        }
		public async Task<ResultDto> Execute(UpdateLessonDto updateLessonDto, int lessonId)
        {
			try
			{
                var lesson = _context.Lessons.Where(l => l.Id == lessonId).FirstOrDefault();

                if (updateLessonDto.TopBannerPhoto != null)
                {
                    var imageSrc = GetImageSrc(updateLessonDto.Title, updateLessonDto.PhotoFileName);
                    await SaveLessonImage(imageSrc, updateLessonDto.TopBannerPhoto);

                    if (lesson.TopBannerSrc != "" && await _fileHandler.IsObjectExist("lessons", lesson.TopBannerSrc))
                    {
                        await _fileHandler.RemoveObject("lessons", lesson.TopBannerSrc);
                    }

                    lesson.TopBannerSrc = imageSrc;
                }

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

        private async Task SaveLessonImage(string imageSrc, IFormFile formFile)
        {
            await _fileHandler.CreateBucket("lessons");
            Stream stream = formFile.OpenReadStream();
            await _fileHandler.SaveStreamObject("lessons", imageSrc, stream, "image/jpg");
        }

        private string GetImageSrc(string lessonTitle, string photoFileName)
        {
            var ImageName = lessonTitle + "_" + DateTime.Now.Ticks.ToString();
            var imgaeSrc = $"({ImageName})" + photoFileName.ToString();

            return MD5Hash.ComputeMD5(imgaeSrc);
        }
    }
}
