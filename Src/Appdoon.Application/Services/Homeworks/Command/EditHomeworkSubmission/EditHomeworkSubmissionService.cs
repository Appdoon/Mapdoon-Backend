using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Progress;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Homeworks.Command.EditHomeworkSubmission
{
	public class EditHomeworkSubmissionDto
	{
		public int HomeworkId { get; set; }
		public string Answer { get; set; }
	}
	public interface IEditHomeworkSubmissionService : ITransientService
	{
		Task<ResultDto<bool>> EditSubmission(EditHomeworkSubmissionDto input, int userId);
	}

	public class EditHomeworkSubmissionService
	{
		private readonly IDatabaseContext _databaseContext;

		public EditHomeworkSubmissionService(IDatabaseContext databaseContext)
        {
			_databaseContext = databaseContext;

		}
        public async Task<ResultDto<bool>> EditSubmission(EditHomeworkSubmissionDto input, int userId)
		{
			try
			{
				// check if submission exists and not done
				var submission = _databaseContext.HomeworkProgresses
					.Where(h => h.HomeworkId == input.HomeworkId && h.UserId == userId)
					.FirstOrDefault();

				if (submission == null)
				{
					// create new submission
					var newSubmission = new HomeworkProgress()
					{
						Answer = input.Answer,
						HomeworkId = input.HomeworkId,
						UserId = userId,
						IsDone = false,
					};

					_databaseContext.HomeworkProgresses.Add(newSubmission);
					await _databaseContext.SaveChangesAsync();

					return new ResultDto<bool>
					{
						IsSuccess = true,
						Message = "تمرین اضافه شد",
						Data = true,
					};
				}

				if(submission.IsDone == false && submission.Score == null)
				{
					submission.Answer = input.Answer;
					await _databaseContext.SaveChangesAsync();

					return new ResultDto<bool>
					{
						IsSuccess = true,
						Message = "تمرین ویرایش شد",
						Data = true,
					};
				}
				else
				{
					return new ResultDto<bool>
					{
						IsSuccess = false,
						Message = "نمره تمرین ثبت شده و امکان ثبت مجدد وجود ندارد!",
						Data = false,
					};
				}
			}
			catch(Exception e)
			{
				return new ResultDto<bool>
				{
					IsSuccess = false,
					Message = e.Message,
				};
			}
		}
	}
}
