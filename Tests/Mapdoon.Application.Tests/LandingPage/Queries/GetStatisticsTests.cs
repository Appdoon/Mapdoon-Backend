using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Application.Services.LandingPage.Query.GetStatisticsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class GetStatisticsTests : TestBase
    {
        [Test]
        public void ShouldGetStatistics()
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
                int homeworkId = AddEntity(new Homework
                {
                    Title = "Title",
                    Question = "Question",
                    MinScore = 1,
                    CreatorId = userId,
                });

                int childStepId = AddEntity(new ChildStep
                {
                    StepId = stepId,
                    HomeworkId = homeworkId,
                });

                GetDatabaseContext().SaveChanges();
            }

            var result = new GetStatisticsService(GetDatabaseContext()).Execute();
            result.IsSuccess.Should().BeTrue();
        }
    }
}
