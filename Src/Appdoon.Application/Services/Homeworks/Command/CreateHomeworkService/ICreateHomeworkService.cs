using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.HomeWorks;
using Mapdoon.Common.Interfaces;
using System;

namespace Appdoon.Application.Services.Homeworks.Command.CreateHomeworkService
{
    public class CreateHomeworkDto
    {
        public string Title { get; set; }
        public string Question { get; set; }
        public int MinScore { get; set; }
        public int ChildStepId { get; set; }
    }
    public interface ICreateHomeworkService : ITransientService
    {
        ResultDto Execute(CreateHomeworkDto homeworkDto, int userId);
    }
    public class CreateHomeworkService : ICreateHomeworkService
    {
        private readonly IDatabaseContext _context;

        public CreateHomeworkService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(CreateHomeworkDto createHomeworkDto, int userId)
        {
            try
            {
                if(_context.ChildSteps.Find(createHomeworkDto.ChildStepId).HomeworkId != null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این محتوا از قبل تمرین دارد!",
                    };
                }

                var homework = new Homework()
                {
                    MinScore = createHomeworkDto.MinScore,
                    Question = createHomeworkDto.Question,
                    Title = createHomeworkDto.Title,
                    CreatorId = userId,
                    ChildStep = _context.ChildSteps.Find(createHomeworkDto.ChildStepId)
                };


                _context.Homeworks.Add(homework);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تمرین اضافه شد.",
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
