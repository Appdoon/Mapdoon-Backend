using Mapdoon.Application.Services.GradeHomeworks.Command.UpdateScoreService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.GradeHomeworks.Command
{
    using static Testing;
    public class UpdateScoreTests
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var submission = new HomeworkProgressUpdateDto
            {
                HomeworkId = 1000,
                UserId = 200,
                Score = 50
            };
            var result = new UpdateScoreService(GetDatabaseContext()).Execute(submission);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldSubmitScore()
        {
            var userId1 = AddEntity(new User
            {
                Email = "aysa@gmail.com",
                Password = "password",
            });
            var userId2 = AddEntity(new User
            {
                Email = "golsa@gmail.com",
                Password = "password",
            });
            var homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                CreatorId = userId1,
                MinScore = 100,
            });
            var submission = new HomeworkProgressUpdateDto
            {
                HomeworkId = homeworkId,
                UserId = userId2,
                Score = 50,
            };

            var result = new UpdateScoreService(GetDatabaseContext()).Execute(submission);
            result.IsSuccess.Should().Be(true);

        }
    }
}
