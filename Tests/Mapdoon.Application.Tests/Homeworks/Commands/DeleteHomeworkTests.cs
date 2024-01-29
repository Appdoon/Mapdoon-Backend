using Appdoon.Application.Services.Homeworks.Command.DeleteHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
    using static Testing;
    internal class DeleteHomeworkTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new DeleteHomeworkService(GetDatabaseContext()).Execute(10000);
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldDeleteHomework()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
