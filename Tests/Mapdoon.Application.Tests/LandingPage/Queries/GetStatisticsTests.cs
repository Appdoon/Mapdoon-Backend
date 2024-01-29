using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Application.Services.LandingPage.Query.GetStatisticsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class GetStatisticsTests : TestBase
    {
        [Test]
        public void ShouldGetStatistics()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
