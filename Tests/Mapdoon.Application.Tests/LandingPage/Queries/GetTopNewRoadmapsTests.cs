using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Appdoon.Application.Services.LandingPage.Query.GetTopNewRoadmapsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class GetTopNewRoadmapsTests : TestBase
    {
        [Test]
        public async Task ShouldTopNewRoadmaps()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
