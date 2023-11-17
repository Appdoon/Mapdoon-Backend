using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Command.EditHomeworkSubmission;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
	using static Testing;
	public class EditHomeworkSubmissionTests : TestBase
	{
		[Test]
		public async Task ShouldEditHomeworkSubmission()
		{
			// Arrange
			var userId = AddEntity(new User
			{
				Email = "test@gmail.com",
				Password = "password",
			});

			var homeworkId = AddEntity(new Homework
			{
				Title = "Title",
				Question = "Question",
				CreatorId = userId,
			});

			var submission = AddEntity(new HomeworkProgress
			{
				Answer = "Answer",
				HomeworkId = homeworkId,
				UserId = userId,
				IsDone = false,
			});

			var submitHomeworkDto = new EditHomeworkSubmissionDto
			{
				HomeworkId = homeworkId,
				Answer = "NewAnswer",
			};

			// Act
			var result = await new EditHomeworkSubmissionService(GetDatabaseContext()).EditSubmission(submitHomeworkDto, userId);

			// Assert
			result.IsSuccess.Should().Be(true);

			var homeworkProgress = GetDatabaseContext().HomeworkProgresses
				.Where(h => h.HomeworkId == homeworkId)
				.FirstOrDefault();

			homeworkProgress.Should().NotBeNull();
			homeworkProgress.UserId.Should().Be(userId);
			homeworkProgress.Answer.Should().Be(submitHomeworkDto.Answer);
			homeworkProgress.IsDone.Should().Be(false);
		}

		[Test]
		public async Task ShouldDenyApplyNewSubmission_WhenHomeworkHasScored()
		{
			// Arrange
			var userId = AddEntity(new User
			{
				Email = "test@gmail.com",
				Password = "password",
			});

			var homeworkId = AddEntity(new Homework
			{
				Title = "Title",
				Question = "Question",
				CreatorId = userId,
			});

			var firstSubmission = new HomeworkProgress
			{
				Answer = "Answer",
				HomeworkId = homeworkId,
				UserId = userId,
				IsDone = true,
				Score = 10,
			};
			var submissionId = AddEntity(firstSubmission);

			var submitHomeworkDto = new EditHomeworkSubmissionDto
			{
				HomeworkId = homeworkId,
				Answer = "NewAnswer",
			};

			// Act
			var result = await new EditHomeworkSubmissionService(GetDatabaseContext()).EditSubmission(submitHomeworkDto, userId);

			// Assert
			result.IsSuccess.Should().Be(false);
			//result.Message.Should().Be("نمره تمرین ثبت شده و امکان ثبت مجدد وجود ندارد!");
			result.Data.Should().Be(false);

			var homeworkProgress = GetDatabaseContext().HomeworkProgresses
				.Where(h => h.HomeworkId == homeworkId)
				.FirstOrDefault();

			homeworkProgress.Should().NotBeNull();
			homeworkProgress.UserId.Should().Be(userId);
			homeworkProgress.Answer.Should().Be(firstSubmission.Answer);
		}

		//[Test]
		//public async Task ShouldCreateNewSubmission_WhenNoSubmissionExsists()
		//{

		//}
	}
}
