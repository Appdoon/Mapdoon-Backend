using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Appdoon.Application.Services.LandingPage.Query.GetTopNewRoadmapsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class GetTopNewRoadmapsTests : TestBase
    {
        [Test]
        public async Task ShouldTopNewRoadmaps()
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

            var result = await new GetTopNewRoadmapsService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(5);
            result.IsSuccess.Should().BeFalse();
        }
    }
}
