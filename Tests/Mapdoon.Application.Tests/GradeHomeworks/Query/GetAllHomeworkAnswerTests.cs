using Mapdoon.Application.Services.GradeHomeworks.Query.GetAllHomeworkAnswerService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Appdoon.Domain.Entities.Progress;
using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;

namespace Mapdoon.Application.Tests.GradeHomeworks.Query
{
    using static Testing;
    public class GetAllHomeworkAnswerTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new GetAllHomeworkAnswerService(GetDatabaseContext()).Execute(10000);
            result.Should().NotBeNull();
        }
        [Test]
        public void ShouldGetHomeworkAnswers()
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
            var userId3 = AddEntity(new User
            {
                Email = "hannah@gmail.com",
                Password = "password",
            });
            var userId4 = AddEntity(new User
            {
                Email = "anita@gmail.com",
                Password = "password",
            });
            var homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                CreatorId = userId1,
                MinScore = 80,
            });
            var homeworkprogressId1 = AddEntity(new HomeworkProgress
            {
                HomeworkId = homeworkId,
                UserId = userId2,
                Score = 50 ,
                IsDone = false,
                Answer = "Answer"
            });
            var homeworkprogressId2 = AddEntity(new HomeworkProgress
            {
                HomeworkId = homeworkId,
                UserId = userId3,
                Score = 100,
                IsDone = true,
                Answer = "Answer"
            });
            var homeworkprogressId3 = AddEntity(new HomeworkProgress
            {
                HomeworkId = homeworkId,
                UserId = userId4,
                Score = 78,
                IsDone = false,
                Answer = "Answer"
            });

            var result = new GetAllHomeworkAnswerService(GetDatabaseContext()).Execute(homeworkId);
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(true);

        }
    }
}
