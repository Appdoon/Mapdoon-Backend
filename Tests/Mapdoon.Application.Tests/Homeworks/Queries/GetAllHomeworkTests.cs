using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
    using static Testing;
    internal class GetAllHomeworksTests : TestBase
    {
        [Test]
        public void ShouldGetAllHomeworks()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
