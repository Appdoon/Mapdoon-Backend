using Appdoon.Application.Services.Users.Command.EditUserService;
using FluentAssertions;

namespace Mapdoon.Application.Tests.Users.Command
{
    using static Testing;
    public class EditUserTest : TestBase
    {
        [Test]
        public async Task ShouldEditUser()
        {
            var userId = AddUser();

            EditUserDto editUserDto = new EditUserDto
            {
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Username = "NewUserName",
            };

            var result = await new EditUserService(GetDatabaseContext(), GetFacadeFileHandler()).Execute(userId, editUserDto);
            result.IsSuccess.Should().Be(true);

            var editedUser = GetDatabaseContext().Users.Find(userId);

            editedUser.FirstName.Should().Be("NewFirstName");
            editedUser.LastName.Should().Be("NewLastName");
            editedUser.Username.Should().Be("NewUserName");
        }
    }
}
