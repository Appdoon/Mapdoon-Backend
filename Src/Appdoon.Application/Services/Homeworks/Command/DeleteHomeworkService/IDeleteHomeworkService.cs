using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;

namespace Appdoon.Application.Services.Homeworks.Command.DeleteHomeworkService
{
    public interface IDeleteHomeworkService : ITransientService
    {
        ResultDto Execute(int id);
    }
    public class DeleteHomeworkService : IDeleteHomeworkService
    {
        private readonly IDatabaseContext _context;

        public DeleteHomeworkService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int id)
        {
            try
            {
                var homework = _context.Homeworks
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (homework == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این آیدی وجود ندارد!",
                    };
                }

                if (homework.ChildStep != null)
                {
                    homework.ChildStep.HomeworkId = null;
                }

                homework.IsRemoved = true;
                homework.UpdateTime = DateTime.Now;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تمرین حدف شد.",
                };
            }
            catch (Exception e)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا در حذف تمرین!",
                };
            }
        }
    }
}
