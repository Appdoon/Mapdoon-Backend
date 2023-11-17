using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Linq;

namespace Appdoon.Application.Services.Lessons.Command.DeleteLessonService
{
	public interface IDeleteLessonService : ITransientService
    {
		ResultDto Execute(int id);
	}

	public class DeleteLessonService : IDeleteLessonService
	{
		private readonly IDatabaseContext _context;

		public DeleteLessonService(IDatabaseContext context)
		{
			_context = context;
		}
		public ResultDto Execute(int id)
		{
			try
			{

				//Existence(id)

				var les = _context.Lessons.Where(s => s.Id == id).FirstOrDefault();
				if (les == null)
				{
					return new ResultDto()
					{
						IsSuccess = false,
						Message = "این آیدی وجود ندارد!",
					};
				}
				les.IsRemoved = true;
				les.UpdateTime = DateTime.Now;
				_context.SaveChanges();

				return new ResultDto()
				{
					IsSuccess = true,
					Message = "مقاله حدف شد.",
				};
			}
			catch (Exception e)
			{
				return new ResultDto()
				{
					IsSuccess = false,
					Message = "خطا در حذف مقاله!",
				};
			}
		}
	}
}
