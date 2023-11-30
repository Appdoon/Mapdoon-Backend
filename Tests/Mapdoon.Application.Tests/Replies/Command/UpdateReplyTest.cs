using Mapdoon.Application.Services.Replies.Command.UpadteReplyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.UpdateCommentService;

namespace Mapdoon.Application.Tests.Replies.Command
{
    using static Testing;
    public class UpdateReplyTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var reply = new UpdateReplyDto
            {
                Text = "updated text"
            };
            var result = new UpdateReplyService(GetDatabaseContext()).Execute(300000, 100000, reply);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldUpdateReply()
        {
            var reply = new UpdateReplyDto
            {
                Text = "updated text"
            };
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
                Text = "text"
            });
            var replyId = AddEntity(new Reply
            {
                UserId = userId,
                commentId = commentId,
                Text = "text"
            });

            var result = new UpdateReplyService(GetDatabaseContext()).Execute(commentId, userId, reply);
            result.IsSuccess.Should().Be(true);

        }
    }
}
