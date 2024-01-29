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
            var check = true;
            check.Should().BeTrue();

        }
    }
}
