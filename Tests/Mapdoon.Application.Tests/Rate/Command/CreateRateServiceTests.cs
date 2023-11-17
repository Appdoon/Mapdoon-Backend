using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
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
            var rate = new CreateRateDto
            {
                Score = 2
            };
            var userId = AddEntity(new User
            {
                Email = "aysa@gmail.com",
                Password = "password",
            });

            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId,
            });

            var result = new CreateRateService(GetDatabaseContext()).Execute(roadmapId, userId , rate);
            result.IsSuccess.Should().Be(true);

        }
    }
}
