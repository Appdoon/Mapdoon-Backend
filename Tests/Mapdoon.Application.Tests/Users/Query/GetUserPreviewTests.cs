using Appdoon.Application.Services.Roadmaps.Query.GetIndividualRoadmapService;
using Appdoon.Application.Services.Users.Query.GetUserPreviewService;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Users.Query
{
    using static Testing;
    public class GetUserPreviewTests : TestBase
    {
        [Test]
        public async Task ShouldGetUserPreviewInfo()
        {
            //var userId = AddEntity(new User
            //{
            //    Email = "test@gmail.com",
            //    Username = "Test",
            //    Password = "password",
            //});

            //var result = await new GetUserPreviewService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(userId);
            //result.IsSuccess.Should().Be(true);
            //result.Data.Username.Should().Be("Test");
        }
    }
}
