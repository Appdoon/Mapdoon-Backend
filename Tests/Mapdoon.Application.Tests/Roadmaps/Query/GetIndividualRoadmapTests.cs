using Appdoon.Application.Services.Roadmaps.Query.GetIndividualRoadmapService;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Roadmaps.Query
{
    using static Testing;
    public class GetIndividualRoadmapTests : TestBase
    {
        [Test]
        public void ShouldReturnIndividualRoadmapAsync()
        {
            //throw new NotImplementedException();
            //// Arrange
            //var userId = AddUser();

            //var roadmapId = AddEntity(new RoadMap
            //{
            //    Title = "Title",
            //    Description = "Description",
            //    CreatoreId = userId,
            //});

            //// Act
            //var roadmap = new GetIndividualRoadMapService(GetDatabaseContext()).Execute(roadmapId);

            //// Assert
            //roadmap.Should().NotBeNull();
            //roadmap.IsSuccess.Should().Be(true);
        }
    }
}
