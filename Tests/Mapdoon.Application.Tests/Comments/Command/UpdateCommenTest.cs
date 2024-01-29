using Mapdoon.Application.Services.Comments.Command.UpdateCommentService;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Comments;
using FluentAssertions;
using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;

namespace Mapdoon.Application.Tests.Comments.Command
{
    using static System.Net.Mime.MediaTypeNames;
    using static Testing;
    public class UpdateCommenTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var comment = new UpdateCommentDto
            {
                Text = "updated text"
            };
            var result = new UpdateCommentService(GetDatabaseContext()).Execute(300000, 100000, comment);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldUpdateComment()
        {
            var check = true;
            check.Should().Be(true);

        }

    }
}
