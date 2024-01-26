using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace Mapdoon.Architecture.Tests.WebApiTests
{
	public class WebApiTests : BaseTest
	{
		[Fact]
		public void ControllerClassesShouldInheritFromController()
		{
			var result = Types.InAssembly(WebApiAssembly)
										  .That()
										  .ResideInNamespaceContaining("Mapdoon.WebApi.Controllers")
										  .Should()
										  .Inherit(typeof(ControllerBase))
										  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}

		[Fact]
		public void ControllerClassesNameShouldEndWithController()
		{
			var result = Types.InAssembly(WebApiAssembly)
										  .That()
										  .ResideInNamespaceContaining("Mapdoon.WebApi.Controllers")
										  .Should()
										  .HaveNameEndingWith("Controller")
										  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}
	}
}
