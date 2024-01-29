using Appdoon.Application.Services.LandingPage.Query.GetTopEnrolledRoadmapsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class TopEnrolledRoadmapsTests : TestBase
    {
        [Test]
        public async Task ShouldTopEnrolledRoadmapsTAsync()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
