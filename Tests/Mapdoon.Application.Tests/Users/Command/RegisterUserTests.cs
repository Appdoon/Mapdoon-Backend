using Appdoon.Application.Services.Users.Command.RegisterUserService;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Users.Command
{
    using static Testing;
    public class RegisterUserTests : TestBase
    {
        [Test]
        public async Task ShouldRegisterUser()
        {
            RequestRegisterUserDto requestRegisterUserDto = new RequestRegisterUserDto
            {
                Email = "email.test.com",
                Username = "Test",
                PhotoFileName = "Test",
                PhoneNumber = "Test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                RePassword = "Test",
                Role = "Admin"
            };

            var result = await new RegisterUserService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(requestRegisterUserDto);
            result.IsSuccess.Should().Be(true);
        }
    }
}
