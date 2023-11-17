using Appdoon.Application.Services.Homeworks.Command.CreateHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class CreateHomeworkTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new CreateHomeworkService(GetDatabaseContext()).Execute(null, 1000000);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldCreateHomework()
        {
            int userId = AddUser();

            int roadmapId = AddEntity(new RoadMap
            {
                CreatoreId = userId,
            });

            int stepId = AddEntity(new Step()
            {
                RoadMapId = roadmapId
            });

            int childStepId = AddEntity(new ChildStep{
                StepId = stepId
            });

            CreateHomeworkDto homeworkDto = new CreateHomeworkDto
            {
                Title = "Test",
                Question = "Test",
                MinScore = 1,
                ChildStepId = childStepId,
            };

            var result = new CreateHomeworkService(GetDatabaseContext()).Execute(homeworkDto, userId);
            result.IsSuccess.Should().BeTrue();

            Homework? homework = GetDatabaseContext().Homeworks.Where(x => x.CreatorId == userId).FirstOrDefault();
            homework.Should().NotBe(null);
            ChildStep? childStep = GetDatabaseContext().ChildSteps.Where(x => x.Id == childStepId).FirstOrDefault();
            childStep.Should().NotBe(null);
            childStep.HomeworkId = homework.Id;
        }
    }
}
