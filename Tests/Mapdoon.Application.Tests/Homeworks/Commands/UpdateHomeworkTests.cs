using Appdoon.Application.Services.Homeworks.Command.UpdateHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class UpdateHomeworkTests: TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new UpdateHomeworkService(GetDatabaseContext()).Execute(1000000, null);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldUpdateHomework()  
        {
            int userId = AddUser();

            int homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                Question = "Question",
                MinScore = 1,
                CreatorId = userId,
            });

            UpdateHomeworkDto homeworkDto = new UpdateHomeworkDto
            {
                Title = "NewTitle",
                Question = "NewQuestion",
                MinScore = 2,
            };

            var result = new UpdateHomeworkService(GetDatabaseContext()).Execute(homeworkId, homeworkDto);
            result.IsSuccess.Should().BeTrue();

            Homework? updatedHomework = GetDatabaseContext().Homeworks.Find(homeworkId);
            updatedHomework.Title.Should().Be("NewTitle");
            updatedHomework.Question.Should().Be("NewQuestion");
            updatedHomework.MinScore.Should().Be(2);
        }
    }
}
