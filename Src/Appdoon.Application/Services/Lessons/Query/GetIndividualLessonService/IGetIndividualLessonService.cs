using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Lessons.Query.GetIndividualLessonService
{
	public class LessonDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string CreatorName { get; set; } = string.Empty;
		public string CreateTime { get; set; }
		public string TopBannerSrc { get; set; }
		public string Text { get; set; } = string.Empty;
		public int CreatorId { get; set; } = 0;
		public bool HasNewSrc { get; set; } = false;
	}
	public interface IGetIndividualLessonService : ITransientService
    {
		Task<ResultDto<LessonDto>> Execute(int id);
	}

	public class GetLessonService : IGetIndividualLessonService
	{
		private readonly IDatabaseContext _context;
        private readonly IFileHandler _fileHandler;

        public GetLessonService(IDatabaseContext context, IFileHandler fileHandler)
		{
			_context = context;
			_fileHandler = fileHandler;
		}
		public async Task<ResultDto<LessonDto>> Execute(int id)
		{
			try
			{
				var lesson = _context.Lessons
					.Where(x => x.Id == id)
					.Select(r => new LessonDto()
					{
						Id = r.Id,
						Title = r.Title,
						CreatorName = r.Creator.Username,
						CreateTime = r.InsertTime.ToString("yyyy/M/dd", new CultureInfo("fa")),
						TopBannerSrc = r.TopBannerSrc,
						Text = r.Text,
						CreatorId = r.CreatorId,
                    }).FirstOrDefault();

                if (lesson != null && lesson.TopBannerSrc != "" && await _fileHandler.IsObjectExist("lessons", lesson.TopBannerSrc))
                {
                    lesson.HasNewSrc = true;
                    lesson.TopBannerSrc = await _fileHandler.GetObjectUrl("lessons", lesson.TopBannerSrc);
                }

                if (lesson == null)
				{
					return new ResultDto<LessonDto>()
					{
						IsSuccess = false,
						Message = "درس یافت نشد!",
						Data = new LessonDto(),
					};
				}

				return new ResultDto<LessonDto>()
				{
					IsSuccess = true,
					Message = "درس ها ارسال شد",
					Data = lesson,
				};
			}
			catch(Exception e)
			{
				return new ResultDto<LessonDto>()
				{
					IsSuccess = false,
					Message = e.Message,
					Data = new LessonDto(),
				};
			}
		}
	}
}
