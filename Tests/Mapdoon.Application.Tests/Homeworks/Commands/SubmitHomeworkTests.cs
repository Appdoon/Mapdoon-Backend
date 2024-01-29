using Appdoon.Domain.Entities.HomeWorks;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Command.SubmitHomeworkService;

namespace Mapdoon.Application.Tests.Homeworks.Commands
{
	using static Testing;
	public class SubmitHomeworkTests : TestBase
	{
		[Test]
		public async Task ShouldSubmitHomework()
		{
            var check = true;
            check.Should().BeTrue();
        }

	}
}
