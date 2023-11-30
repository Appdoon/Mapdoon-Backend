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
            var userId = AddEntity(new User
            {
                Email = "golsa@gmail.com",
                Password = "password",
            });

            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId,
            });
            var commentId = AddEntity(new Comment
            {
                UserId = userId,
                RoadmapId = roadmapId,
                Text = "text comment"
            });
            var replyId = AddEntity(new Reply
            {
                UserId = userId,
                commentId = commentId,
                Text = "text reply"
            });

            var result = new DeleteReplyService(GetDatabaseContext()).Execute(replyId);
            result.IsSuccess.Should().Be(true);
            Reply? deletedreply = GetDatabaseContext().Replies.Find(replyId);
            deletedreply.IsRemoved.Should().BeTrue();

        }
    }
}
