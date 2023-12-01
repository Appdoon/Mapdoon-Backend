using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions
{

	public class GetUserSubmissionDto
	{
		public int HoemworkId { get; set; }
	}

	public class GetUserSubmissionResult
	{
		public string Answer { get; set; }
		public decimal? Score { get; set; }
		public bool IsDone { get; set; }
	}

	public interface IGetHomeworkSubmissionsService : ITransientService
	{
		Task<ResultDto<GetUserSubmissionResult>> GetUserSubmission(GetUserSubmissionDto input, int userId);
	}

	public class GetHomeworkSubmissionsService : IGetHomeworkSubmissionsService
	{
		private readonly IDatabaseContext _databaseContext;

		public GetHomeworkSubmissionsService(IDatabaseContext databaseContext)
        {
			_databaseContext = databaseContext;

		}
        public async Task<ResultDto<GetUserSubmissionResult>> GetUserSubmission(GetUserSubmissionDto input, int userId)
		{
			try
			{
				var homeworkProgress = await _databaseContext.HomeworkProgresses
					.Where(x => x.HomeworkId == input.HoemworkId && x.UserId == userId)
					.FirstOrDefaultAsync();

				if (homeworkProgress == null)
				{
					return new ResultDto<GetUserSubmissionResult>()
					{
						IsSuccess = true,
						Message = "پاسخی برای تمرین تحویل داده نشده است!",
						Data = null,
					};
				}

				var result = new GetUserSubmissionResult()
				{
					Answer = homeworkProgress.Answer,
					IsDone = homeworkProgress.IsDone,
					Score = homeworkProgress.Score,
				};

				return new ResultDto<GetUserSubmissionResult>()
				{
					IsSuccess = true,
					Message = "پاسخ تمرین با موفقیت دریافت شد!",
					Data = result,
				};
			}
			catch(Exception e)
			{
				return new ResultDto<GetUserSubmissionResult>()
				{
					IsSuccess = false,
					Message = e.Message,
					Data = null,
				};
			}
		}
	}
}
