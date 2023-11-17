using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
    using static Testing;
    internal class GetHomeworkTests : TestBase
    {

        [Test]
        public void ShouldGetHomework()
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
