using Appdoon.Application.Services.Homeworks.Command.DeleteHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class DeleteHomeworkTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new DeleteHomeworkService(GetDatabaseContext()).Execute(10000);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldDeleteHomework()
        {
            int userId = AddUser();

            int homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                Question = "Question",
                MinScore = 1,
                CreatorId = userId,
            });

            var result = new DeleteHomeworkService(GetDatabaseContext()).Execute(homeworkId);
            result.IsSuccess.Should().BeTrue();

            Homework? deletedHomework = GetDatabaseContext().Homeworks.Find(homeworkId);
            deletedHomework.IsRemoved.Should().BeTrue();
        }
    }
}
