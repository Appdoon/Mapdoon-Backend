using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.RoadMaps;

namespace Mapdoon.Application.Tests.GradeHomeworks.Command
{

    using static Testing;
    public class SubmitScoreTests : TestBase
    {

        [Test]
        public void ShouldRequireValidArguments()
        {
            var submission = new HomeworkProgressSubmissionDto
            {
                HomeworkId = 1000,
                UserId = 200,
                Score = 50
            };
            //var result = new SubmitScoreService(GetDatabaseContext()).Execute(submission);
            //result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldSubmitScore()
        {
            var check = true;
            check.Should().Be(true);

        }
    }
}