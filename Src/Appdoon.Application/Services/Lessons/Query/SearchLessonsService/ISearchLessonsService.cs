using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Lessons.Query.SearchLessonsService
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; }
        public string TopBannerSrc { get; set; } = string.Empty;
        public bool HasNewSrc { get; set; } = false;
    }

    public class AllLessonsDto
    {
        public List<LessonDto> Lessons { get; set; }
        public int RowCount { get; set; }
    }
    public interface ISearchLessonsService : ITransientService
    {
        Task<ResultDto<AllLessonsDto>> Execute(string searched_text, int page_number, int page_size);
    }
    public class SearchLessonsService : ISearchLessonsService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public SearchLessonsService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }

        public async Task<ResultDto<AllLessonsDto>> Execute(string searched_text, int page_number, int page_size)
        {
            try
            {
                int rowCount = 0;
                var lessons = _context.Lessons
                    .Where(r => r.Title.Contains(searched_text))
                    .Select(r => new LessonDto()
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Text = r.Text,
                        TopBannerSrc = r.TopBannerSrc
                    }).ToPaged(page_number, page_size, out rowCount)
                    .ToList();

                foreach (var lesson in lessons)
                {
                    string url = await _facadeFileHandler.GetFileUrl("lessons", lesson.TopBannerSrc);
                    lesson.HasNewSrc = (url != lesson.TopBannerSrc);
                    lesson.TopBannerSrc = url;
                }

                AllLessonsDto allLessonsDto = new AllLessonsDto();
                allLessonsDto.Lessons = lessons;
                allLessonsDto.RowCount = rowCount;


                return new ResultDto<AllLessonsDto>()
                {
                    IsSuccess = true,
                    Message = "مقالات پیدا و ارسال شد",
                    Data = allLessonsDto,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllLessonsDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new AllLessonsDto(),
                };
            }
        }
    }

}
