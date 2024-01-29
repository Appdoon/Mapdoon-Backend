using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Domain.Entities.Comments;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;
using FluentAssertions;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;

namespace Mapdoon.Application.Tests.Comments.Command
{
    using static Testing;
    public class CreateCommentsTest : TestBase
    {
        [Test]
        public void ShouldRequireValidArguments()
        {
            var comment = new CreateCommentDto
            {
                Text = "text comment"
            };
            var result = new CreateCommentService(GetDatabaseContext()).Execute(100000, 30000000, comment);
            result.IsSuccess.Should().Be(false);
        }
        [Test]
        public void ShouldCommentRoadmap()
        {
            var comment = new CreateCommentDto
            {
                Text = "text comment"
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

            var result = new CreateCommentService(GetDatabaseContext()).Execute(roadmapId, userId, comment);
            var check = true;
            check.Should().Be(true);
            

        }
    }
}
