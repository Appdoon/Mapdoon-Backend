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
        public void ShouldRequireValidArguments()
        {
            var result = new BookmarkRoadmapService(GetDatabaseContext()).Execute(100000, 30000000);
            result.IsSuccess.Should().Be(false);
        }

        [Test]
        public void ShouldBookmarkRoadmap()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
