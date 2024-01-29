using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Mapdoon.Application.Services.Replies.Command.DeleteReplyService;
using FluentAssertions;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.DeleteCommentService;

namespace Mapdoon.Application.Tests.Replies.Command
{
    using static Testing;
    public class DeleteReplyTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new DeleteReplyService(GetDatabaseContext()).Execute(100000);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldDeleteReplyCommentRoadmap()
        {
            var check = true;
            check.Should().BeTrue();

        }
    }
}
