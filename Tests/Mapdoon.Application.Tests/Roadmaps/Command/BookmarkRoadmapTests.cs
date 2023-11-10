using Appdoon.Application.Services.RoadMaps.Command.BookmarkRoadmapService;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Roadmaps.Command
{
    using static Testing;
    public class BookmarkRoadmapTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Can't Run This Test Because Of Our Execute Arguments Design
            //FluentActions.Invoking(() => new BookmarkRoadmapService(GetDatabaseContext()).Execute(0,0))
            //    .Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new BookmarkRoadmapService(GetDatabaseContext()).Execute(100000, 30000000);
            result.IsSuccess.Should().Be(false);
        }

        [Test]
        public void ShouldBookmarkRoadmap()
        {
            var userId = AddEntity(new User
            {
                Email = "arman@gmail.com",
                Password = "password",
            });

            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId,
            });

            var result = new BookmarkRoadmapService(GetDatabaseContext()).Execute(roadmapId, userId);
            result.IsSuccess.Should().Be(true);

            var userBookmarkedRoadmaps = GetDatabaseContext().Users.Find(userId).BookmarkedRoadMaps;

            var bookmarkedRoadmap = userBookmarkedRoadmaps
                .Where(r => r.Id == roadmapId)
                .FirstOrDefault();

            bookmarkedRoadmap.Should().NotBeNull();
            bookmarkedRoadmap.InsertTime.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
        }
    }
}
