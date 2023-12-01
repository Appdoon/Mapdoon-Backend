using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.Replies;
using Appdoon.Domain.Entities.Comments;
using Mapdoon.Application.Services.Replies.Command.CreateReplyService;
using FluentAssertions;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;

namespace Mapdoon.Application.Tests.Replies.Command
{
    using static Testing;
    public class CreateReplyTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var reply = new CreateReplyDto
            {
                Text = "text comment"
            };
            var result = new CreateReplyService(GetDatabaseContext()).Execute(100000, 30000000, reply);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldReplyCommentRoadmap()
        {

            var userId1 = AddEntity(new User
            {
                Email = "golsa@gmail.com",
                Password = "password",
            });
            var userId2 = AddEntity(new User
            {
                Email = "aysa@gmail.com",
                Password = "password",
            });
            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId1,
            });
            var commentId = AddEntity(new Comment
            {
                UserId = userId2,
                RoadmapId = roadmapId,
                Text = "text"
            });
            var reply = new CreateReplyDto
            {
                Text = "text comment"
            };

            var result = new CreateReplyService(GetDatabaseContext()).Execute(commentId, userId1, reply);
            result.IsSuccess.Should().Be(true);

        }
    }
}
