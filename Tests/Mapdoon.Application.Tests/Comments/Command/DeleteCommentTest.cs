using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.Comments;
using Mapdoon.Application.Services.Comments.Command.DeleteCommentService;
using FluentAssertions;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;

namespace Mapdoon.Application.Tests.Comments.Command
{
    using static Testing;
    public class DeleteCommentTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var result = new DeleteCommnetService(GetDatabaseContext()).Execute(100000);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldDeleteCommentRoadmap()
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

            var result = new DeleteCommnetService(GetDatabaseContext()).Execute(commentId);
            result.IsSuccess.Should().Be(true);
            Comment? deletedcomment = GetDatabaseContext().Comments.Find(commentId);
            deletedcomment.IsRemoved.Should().BeTrue();

        }
    }
}
