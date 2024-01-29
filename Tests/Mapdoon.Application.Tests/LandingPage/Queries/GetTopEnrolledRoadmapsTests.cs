using Appdoon.Application.Services.LandingPage.Query.GetTopEnrolledRoadmapsService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions;

namespace Mapdoon.Application.Tests.LandingPage.Queries
{
    using static Testing;
    internal class TopEnrolledRoadmapsTests : TestBase
    {
        [Test]
        public async Task ShouldTopEnrolledRoadmapsTAsync()
        {
            var userId = AddEntity(new User
            {
                Email = "test@gmail.com",
                Password = "password",
            });

            var homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                Question = "Question",
                CreatorId = userId,
            });

            var submitHomeworkDto = new GetUserSubmissionDto
            {
                HoemworkId = homeworkId,
            };

            // Act
            var result = await new GetTopEnrolledRoadmapsService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(5);

            // Assert
            result.IsSuccess.Should().Be(true);
        }
    }
}
