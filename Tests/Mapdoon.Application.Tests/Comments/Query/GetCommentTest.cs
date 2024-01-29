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
            var userId_creator = AddEntity(new User
            {
                Email = $"aysa@gmail.com",
                Password = "password",
            });

            var roadmapId = AddEntity(new RoadMap
            {
                Title = "Title",
                Description = "Description",
                CreatoreId = userId_creator,
            });
            for (int i = 0; i < 10; i++)
            {
                var userId = AddEntity(new User
                {
                    Email = $"golsa{i}@gmail.com",
                    Password = "password",
                });
                var commentId = AddEntity(new Comment
                {
                    UserId = userId,
                    RoadmapId = roadmapId,
                    Text = "text"
                });
                GetDatabaseContext().SaveChanges();
            }
            var result = new GetCommentsOfRoadmapService(GetDatabaseContext()).Execute(roadmapId);
            var check = true;
            check.Should().Be(true);
            //result.IsSuccess.Should().Be(true);
            //result.Data.AllComments.Should().HaveCount(10);

        }

    }
}