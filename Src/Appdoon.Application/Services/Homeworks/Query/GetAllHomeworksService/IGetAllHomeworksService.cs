using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService
{
    public class HomeworkDtoAll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal MinScore { get; set; }
        public string Question { get; set; }
        public int CreatorId { get; set; }
    }
    public class AllHomeworksDto
    {
        public List<HomeworkDtoAll> Homeworks { get; set; }
        public int RowCount { get; set; }
    }
    public interface IGetAllHomeworksService : ITransientService
    {
        public ResultDto<AllHomeworksDto> Execute(int page_number, int page_size);
    }

    public class GetAllHomeworksService : IGetAllHomeworksService
    {
        private readonly IDatabaseContext _context;

        public GetAllHomeworksService(IDatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public ResultDto<AllHomeworksDto> Execute(int page_number, int page_size)
        {
            try
            {
                int rowCount = 0;
                var homeworks = _context.Homeworks.Select(h => new HomeworkDtoAll
                {
                    Id = h.Id,
                    Title = h.Title,
                    MinScore = h.MinScore,
                    Question = h.Question,
                    CreatorId = h.CreatorId,
                }).ToPaged(page_number, page_size, out rowCount)
                .ToList();
                AllHomeworksDto allHomeworksDto = new AllHomeworksDto();
                allHomeworksDto.Homeworks = homeworks;
                allHomeworksDto.RowCount = rowCount;

                return new ResultDto<AllHomeworksDto>()
                {
                    IsSuccess = true,
                    Message = "دسته‌بندی‌ها ارسال شدند.",
                    Data = allHomeworksDto
                };

            }
            catch (Exception e)
            {
                return new ResultDto<AllHomeworksDto>()
                {
                    IsSuccess = false,
                    Message = "ارسال ناموفق دسته‌بندی‌ها!",
                    Data = new AllHomeworksDto()
                };
            }
        }
    }
}
