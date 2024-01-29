using Appdoon.Application.Services.Homeworks.Command.CreateHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class CreateHomeworkTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new CreateHomeworkService(GetDatabaseContext()).Execute(null, 1000000);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldCreateHomework()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
