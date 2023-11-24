using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
    using static Testing;
    internal class GetAllHomeworksTests : TestBase
    {
        [Test]
        public void ShouldGetAllHomeworks()
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

            for (int i = 0; i < 10; i++)
            {
                int childStepId = AddEntity(new ChildStep
                {
                    StepId = stepId
                }); ;

                int homeworkId = AddEntity(new Homework
                {
                    Title = "Title",
                    Question = "Question",
                    MinScore = 1,
                    CreatorId = userId,
                });

                GetDatabaseContext().ChildSteps.Find(childStepId).HomeworkId = homeworkId;
                GetDatabaseContext().SaveChanges();
            }

            var result = new GetAllHomeworksService(GetDatabaseContext()).Execute();
            result.IsSuccess.Should().BeTrue();
            result.Data.Homeworks.Should().HaveCount(10);
        }
    }
}
