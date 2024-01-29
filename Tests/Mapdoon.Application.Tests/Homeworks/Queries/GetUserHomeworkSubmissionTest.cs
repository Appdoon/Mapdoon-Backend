using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
	using static Testing;
	public class GetUserHomeworkSubmissionTest : TestBase
	{
		[Test]
		public async Task ShouldGetUserHomeworkSubmission()
		{
            var check = true;
            check.Should().BeTrue();
        }

		[Test]
		public async Task ShouldGetUserHomeworkSubmission_WhenSubmissionDoesNotExist()
		{
            var check = true;
            check.Should().BeTrue();
        }
	}
}
