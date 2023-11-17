using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
	using static Testing;
	public class GetUserHomeworkSubmissionTest : TestBase
	{
		[Test]
		public async Task ShouldGetUserHomeworkSubmission()
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

			var submission = new HomeworkProgress
			{
				Answer = "Answer",
				HomeworkId = homeworkId,
				UserId = userId,
				IsDone = false,
			};

			var submissionId = AddEntity(submission);

			var submitHomeworkDto = new GetUserSubmissionDto
			{
				HoemworkId = homeworkId,
			};

			// Act
			var result = await new GetHomeworkSubmissionsService(GetDatabaseContext()).GetUserSubmission(submitHomeworkDto, userId);

			// Assert
			result.IsSuccess.Should().Be(true);
			result.Data.Should().NotBeNull();
			result.Data.Answer.Should().Be(submission.Answer);
		}

		[Test]
		public async Task ShouldGetUserHomeworkSubmission_WhenSubmissionDoesNotExist()
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

			var submitHomeworkDto = new GetUserSubmissionDto
			{
				HoemworkId = homeworkId,
			};

			// Act
			var result = await new GetHomeworkSubmissionsService(GetDatabaseContext()).GetUserSubmission(submitHomeworkDto, userId);

			// Assert
			result.IsSuccess.Should().Be(true);
			result.Data.Should().BeNull();
		}
	}
}
