using Appdoon.Application.Services.Users.Query.GetUserService;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Users.Query
{
    using static Testing;
    public class GetUserTests : TestBase
    {
        [Test]
        public async Task ShouldGetUserInfo()
        {
            //var userId = AddEntity(new User
            //{
            //    Email = "test@gmail.com",
            //    Username = "Test",
            //    Password = "password",
            //});

            //var result = await new GetUserService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(userId);
            //result.IsSuccess.Should().Be(true);

            //result.Data.Email.Should().Be("test@gmail.com");
            //result.Data.Username.Should().Be("Test");
        }
    }
}
