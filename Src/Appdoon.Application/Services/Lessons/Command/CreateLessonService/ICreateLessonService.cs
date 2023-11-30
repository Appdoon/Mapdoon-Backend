using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.HashFunctions;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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
        private readonly IFileHandler _fileHandler;
        private readonly IDatabaseContext _context;

        public CreateLessonService(IDatabaseContext context,
            IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
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

                string imageSrc = "";
                if(createLessonDto.TopBannerPhoto != null)
                {
                    imageSrc = GetImageSrc(createLessonDto.Title, createLessonDto.PhotoFileName);
                    await SaveLessonImage(imageSrc, createLessonDto.TopBannerPhoto);
                }

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
