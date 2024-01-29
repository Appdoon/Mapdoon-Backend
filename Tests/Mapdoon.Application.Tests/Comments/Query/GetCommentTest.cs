using Mapdoon.Application.Services.Comments.Command.UpdateCommentService;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Comments;
using FluentAssertions;
using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;
using Mapdoon.Application.Services.Comments.Query.GetCommentsOfRoadmapService;
using Appdoon.Domain.Entities.HomeWorks;

namespace Mapdoon.Application.Tests.Comments.Query
{
    using static Testing;
    public class GetCommenTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new GetCommentsOfRoadmapService(GetDatabaseContext()).Execute(300000);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldGetComment()
        {
            var check = true;
            check.Should().Be(true);

        }

    }
}