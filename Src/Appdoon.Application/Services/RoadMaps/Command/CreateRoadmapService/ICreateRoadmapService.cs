using Appdoon.Application.Interfaces;
using Appdoon.Application.Validatores.RoadMapValidatore;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Roadmaps.Command.CreateRoadmapService
{
    public class CreateRoadmapDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoFileName { get; set; }
        public IFormFile RoadmapPhoto { get; set; }
        public List<string> CategoryNames { get; set; }
        public float? Price { get; set; }
    }
    public interface ICreateRoadmapService : ITransientService
    {
        Task<ResultDto> Execute(CreateRoadmapDto createRoadmapDto, int userId);
    }
    public class CreateRoadmapService : ICreateRoadmapService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public CreateRoadmapService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto> Execute(CreateRoadmapDto createRoadmapDto, int userId)
        {
            try
            {
                if (_context.RoadMaps.Any(s => s.Title == createRoadmapDto.Title.ToString()) == true)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این نام برای رودمپ تکراری است",
                    };
                }

                List<Category> categories = new List<Category>();
                if (createRoadmapDto.CategoryNames.Count != 0)
                {
                    foreach (var item in createRoadmapDto.CategoryNames)
                    {
                        Category category = _context.Categories.Where(s => s.Name == item).FirstOrDefault();
                        if (category != null)
                            categories.Add(category);
                    }
                }

                var creator = _context.Users
                    .Where(c => c.Id == userId)
                    .FirstOrDefault();

                if (creator == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر پیدا نشد!",
                    };
                }

                string imageSrc = await _facadeFileHandler.CreateFile(
                    createRoadmapDto.Title,
                    createRoadmapDto.PhotoFileName,
                    "roadmaps", "image/jpg",
                    createRoadmapDto.RoadmapPhoto
                    );

                var roadmap = new RoadMap()
                {
                    Title = createRoadmapDto.Title,
                    Description = createRoadmapDto.Description,
                    ImageSrc = imageSrc,
                    Categories = categories,
                    Creatore = creator,
                    Price = createRoadmapDto.Price
                };

                // validate inputes
                RoadMapValidatore validationRules = new RoadMapValidatore();
                var result = validationRules.Validate(roadmap);

                if (result.IsValid == false)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = result.Errors[0].ErrorMessage,
                    };
                }

                _context.RoadMaps.Add(roadmap);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "رودمپ ساخته شد",
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
        #region Ajax Get Image
        /*
		private static string ExtractImages(IFormFile image, IHostingEnvironment environment)
		{
			var uploadResult = UploadFile(image, environment);
			return uploadResult.FileNameAddress;
		}

		private static UploadDto UploadFile(IFormFile file, IHostingEnvironment environment)
		{
			if(file != null)
			{
				string folder = $@"images\roadmaps\";
				var uploadsRootFolder = Path.Combine(environment.WebRootPath, folder);
				if(Directory.Exists(uploadsRootFolder) == false)
				{
					Directory.CreateDirectory(uploadsRootFolder);
				}

				if(file == null || file.Length == 0)
				{
					return new UploadDto()
					{
						Status = false,
						FileNameAddress = string.Empty,
					};
				}

				string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
				var filePath = Path.Combine(uploadsRootFolder, fileName);
				using(var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}

				return new UploadDto()
				{
					Status = true,
					FileNameAddress = folder + fileName,
				};
			}
			else
			{
				return new UploadDto()
				{
					Status = false,
					FileNameAddress = string.Empty,
				};
			}
		}
		*/
        #endregion
    }
}
