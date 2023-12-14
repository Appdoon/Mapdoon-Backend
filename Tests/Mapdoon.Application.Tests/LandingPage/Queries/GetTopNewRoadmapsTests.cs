using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class GetTopNewRoadmapsTests : TestBase
    {
        [Test]
        public void ShouldTopNewRoadmaps()
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

            var result = new GetHomeworkService(GetDatabaseContext()).Execute(homeworkId);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }
    }
}
