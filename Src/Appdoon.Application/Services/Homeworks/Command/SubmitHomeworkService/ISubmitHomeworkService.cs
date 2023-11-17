using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Progress;
using Mapdoon.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Homeworks.Command.SubmitHomeworkService
{
	public class SubmitHomeworkDto
	{
		public int HomeworkId { get; set; }
		public string Answer { get; set; }
	}
	public interface ISubmitHomeworkService
	{
		Task<ResultDto> SubmitHomework(SubmitHomeworkDto submitHomeworkDto, int userId);
	}

	public class SubmitHomeworkService : ISubmitHomeworkService
	{
		private readonly IDatabaseContext _databaseContext;

		public SubmitHomeworkService(IDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		public async Task<ResultDto> SubmitHomework(SubmitHomeworkDto submitHomeworkDto, int userId)
		{
			try
			{
				var homeworkPorgress = new HomeworkProgress()
				{
					HomeworkId = submitHomeworkDto.HomeworkId,
					Answer = submitHomeworkDto.Answer,
					UserId = userId,
					IsDone = false,
				};

				await _databaseContext.HomeworkProgresses.AddAsync(homeworkPorgress);
				await _databaseContext.SaveChangesAsync();

				return new ResultDto()
				{
					IsSuccess = true,
					Message = "پاسخ تمرین ارسال شد.",
				};
			}
			catch(Exception ex)
			{
				return new ResultDto()
				{
					IsSuccess = false,
					Message = "خطا در ارسال پاسخ تمرین!",
				};
			}
		}
	}
}
