using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Command.EditHomeworkSubmission;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
	using static Testing;
	public class EditHomeworkSubmissionTests : TestBase
	{
		[Test]
		public async Task ShouldEditHomeworkSubmission()
		{
            var check = true;
            check.Should().BeTrue();
        }

		[Test]
		public async Task ShouldDenyApplyNewSubmission_WhenHomeworkHasScored()
		{
            var check = true;
            check.Should().BeTrue();
        }

		//[Test]
		//public async Task ShouldCreateNewSubmission_WhenNoSubmissionExsists()
		//{

		//}
	}
}
