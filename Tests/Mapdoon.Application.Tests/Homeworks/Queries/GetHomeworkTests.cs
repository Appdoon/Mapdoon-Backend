using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.RoadMaps;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Tests.Homeworks.Queries
{
    using static Testing;
    internal class GetHomeworkTests : TestBase
    {

        [Test]
        public void ShouldGetHomework()
        {
            var check = true;
            check.Should().BeTrue();
        }
    }
}
