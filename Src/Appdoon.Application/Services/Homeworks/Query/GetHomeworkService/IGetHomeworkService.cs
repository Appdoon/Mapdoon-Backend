using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;

namespace Appdoon.Application.Services.Homeworks.Query.GetHomeworkService
{
    public class NewHomeworkDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public decimal MinScore { get; set; }
        public int CreatorId { get; set; }
        public int ChildStepId { get; set; }
    }
    public interface IGetHomeworkService : ITransientService
    {
        ResultDto<NewHomeworkDto> Execute(int id);
    }
    public class GetHomeworkService : IGetHomeworkService
    {
        private readonly IDatabaseContext _context;
        public GetHomeworkService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<NewHomeworkDto> Execute(int id)
        {
            try
            {
                var homework = _context.Homeworks
                    .Where(h => h.Id == id)
                    .Select(h => new NewHomeworkDto
                    {
                        Id = h.Id,
                        Title = h.Title,
                        Question = h.Question,
                        MinScore = h.MinScore,
                        CreatorId = h.CreatorId,
                        ChildStepId = h.ChildStep.Id
                    })
                    .FirstOrDefault();
                return new ResultDto<NewHomeworkDto>()
                {
                    IsSuccess = true,
                    Message = "تکالیف ارسال شدند.",
                    Data = homework
                };
            }
            catch (Exception e)
            {
                return new ResultDto<NewHomeworkDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new NewHomeworkDto()
                };
            }
        }
    }
}
