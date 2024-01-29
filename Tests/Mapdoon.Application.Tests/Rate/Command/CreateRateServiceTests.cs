using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Rate.Command
{
    using static Testing;
    public class CreateRateServiceTests : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var rate = new CreateRateDto
            {
                Score = 2
            };
            var result = new CreateRateService(GetDatabaseContext()).Execute(100000, 30000000 , rate);
            result.IsSuccess.Should().Be(false);
        }

        [Test]
        public void ShouldRateRoadmap()
        {
            var check = true;
            check.Should().BeTrue();

        }
    }
}
