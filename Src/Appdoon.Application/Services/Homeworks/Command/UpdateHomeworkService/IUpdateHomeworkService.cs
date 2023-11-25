using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;

namespace Appdoon.Application.Services.Homeworks.Command.UpdateHomeworkService
{
    public class UpdateHomeworkDto
    {
        public string Title { get; set; }
        public string Question { get; set; }
        public int MinScore { get; set; }
    }
    public interface IUpdateHomeworkService : ITransientService
    {
        ResultDto Execute(int id, UpdateHomeworkDto updateHomeworkDto);
    }
    public class UpdateHomeworkService : IUpdateHomeworkService
    {
        private readonly IDatabaseContext _context;

        public UpdateHomeworkService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int id, UpdateHomeworkDto updateHomeworkDto)
        {
            try
            {
                var homework = _context.Homeworks
                    .Where(h => h.Id == id)
                    .FirstOrDefault();

                homework.UpdateTime = DateTime.Now;
                homework.MinScore = updateHomeworkDto.MinScore;
                homework.Question = updateHomeworkDto.Question;
                homework.Title = updateHomeworkDto.Title;

                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تمرین بروزرسانی شد.",
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
