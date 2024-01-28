using Mapdoon.Application.Services.GradeHomeworks.Command.UpdateScoreService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.RoadMaps;

namespace Mapdoon.Application.Tests.GradeHomeworks.Command
{
    using static Testing;
    public class UpdateScoreTests : TestBase
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
            //var result = new UpdateScoreService(GetDatabaseContext()).Execute(submission);
            //result.IsSuccess.Should().Be(false);
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
            var homeworkprogressId = AddEntity(new HomeworkProgress
            {
                HomeworkId = homeworkId,
                UserId = userId2,
                Score = 0,
                IsDone = false,
                Answer = "Answer"
            });
            var submission = new HomeworkProgressUpdateDto
            {
                HomeworkId = homeworkId,
                UserId = userId2,
                Score = 50,
            };
            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId1,
            });
            var step = AddEntity(new Step
            {
                Title = "title",
                Description = "Description",
                RoadMapId = roadmapId
            });
            var childstep = AddEntity(new ChildStep
            {
                Title = "title",
                Description = "Description",
                HomeworkId = homeworkId,
                StepId = step
            });
            var childstepprogress = AddEntity(new ChildStepProgress
            {
                ChildStepId = childstep,
                UserId = userId1
            });

            var result = new UpdateScoreService(GetDatabaseContext(), GetNotificationService()).Execute(submission);
            result.IsSuccess.Should().Be(false);

        }
    }
}
