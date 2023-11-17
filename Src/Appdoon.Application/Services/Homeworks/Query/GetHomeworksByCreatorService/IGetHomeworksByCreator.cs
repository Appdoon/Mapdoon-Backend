using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Homeworks;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapdoon.Application.Services.Homeworks.Query.GetHomeworksByCreatorService
{
    public class HomeworkDto
    {
        public int HomeworkId { get; set; }
        public string Title { get; set; }
        public int ChildStepId { get; set; }
    }
    public class AllHomeworksDto
    {
        public List<HomeworkDto> Homeworks { get; set; }
    }

    public interface IGetHomeworksByCreator : ITransientService
    {
        public ResultDto<AllHomeworksDto> Execute(int creatorId);
    }
    public class GetHomeWorksByCreator : IGetHomeworksByCreator
    {
        private readonly IDatabaseContext _context;
        public GetHomeWorksByCreator(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<AllHomeworksDto> Execute(int creatorId)
        {
            try
            {
                var homeworks = _context.Homeworks
                    .Where(h => h.CreatorId == creatorId)
                    .Select(h => new HomeworkDto
                    {
                        HomeworkId = h.Id,
                        Title = h.Title,
                        ChildStepId = h.ChildStep.Id
                    })
                    .ToList();
                AllHomeworksDto allHomeworksDto = new AllHomeworksDto();
                allHomeworksDto.Homeworks = homeworks;
                return new ResultDto<AllHomeworksDto>()
                {
                    IsSuccess = true,
                    Message = "تکالیف ارسال شدند.",
                    Data = allHomeworksDto
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllHomeworksDto>()
                {
                    IsSuccess = false,
                    Message = "ارسال ناموفق ‌‌تکالیف!",
                    Data = new AllHomeworksDto()
                };
            }
        }

    }
}
