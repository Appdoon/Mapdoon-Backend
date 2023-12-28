namespace Mapdoon.Application.Tests.Users.Query
{
    using Appdoon.Application.Services.Users.Query.GetUserFromCookieService;
    using Appdoon.Domain.Entities.Users;
    using FluentAssertions;
    using static Testing;
    public class GetUserFromCookieTests : TestBase
    {
        [Test]
        public async Task ShouldGetUserInfoFromCookie()
        {
            var userId = AddEntity(new User
            {
                Email = "test@gmail.com",
                Username = "Test",
                Password = "password",
            });

            var result = await new GetUserFromCookieService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(userId);
            result.IsSuccess.Should().Be(true);

            result.Data.Email.Should().Be("test@gmail.com");
            result.Data.Username.Should().Be("Test");
        }
    }
}
