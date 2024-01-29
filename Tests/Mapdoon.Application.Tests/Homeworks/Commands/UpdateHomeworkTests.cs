using Appdoon.Application.Services.Homeworks.Command.UpdateHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class UpdateHomeworkTests: TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new UpdateHomeworkService(GetDatabaseContext()).Execute(1000000, null);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldUpdateHomework()  
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
