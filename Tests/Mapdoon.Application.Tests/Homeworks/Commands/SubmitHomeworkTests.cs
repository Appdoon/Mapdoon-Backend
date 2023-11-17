using Appdoon.Domain.Entities.HomeWorks;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Command.SubmitHomeworkService;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
	using static Testing;
	public class SubmitHomeworkTests : TestBase
	{
		[Test]
		public async Task ShouldSubmitHomework()
		{
			// Arrange
			var userId = AddUser();

			var homeworkId = AddEntity(new Homework
			{
				Title = "Title",
				Question = "Question",
				CreatorId = userId,
			});

			var submitHomeworkDto = new SubmitHomeworkDto
			{
				HomeworkId = homeworkId,
				Answer = "Answer",
			};

			// Act
			var result = await new SubmitHomeworkService(GetDatabaseContext()).SubmitHomework(submitHomeworkDto, userId);

			// Assert
			result.IsSuccess.Should().Be(true);

			var homeworkProgress = GetDatabaseContext().HomeworkProgresses
				.Where(h => h.HomeworkId == homeworkId)
				.FirstOrDefault();

			homeworkProgress.Should().NotBeNull();
			homeworkProgress.Answer.Should().Be("Answer");
			homeworkProgress.IsDone.Should().Be(false);
			homeworkProgress.UserId.Should().Be(userId);
		}

	}
}
