using FluentAssertions;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Architecture.Tests.LayersTests
{
	public class LayersTests : BaseTest
	{

		[Fact]
		public void ShouldDomainHasNoDependencyOnApplication()
		{
			var result = Types.InAssembly(DomainAssembly)
				              .Should()
							  .NotHaveDependencyOn("Mapdoon.Application")
							  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}

		[Fact]
		public void ShouldApplicationHasNoDependencyOnWebApi()
		{
			var result = Types.InAssembly(ApplicationAssembly)
							  .Should()
							  .NotHaveDependencyOn("Mapdoon.WebApi")
							  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}

		[Fact]
		public void ShouldPersistenceHasNoDependencyOnWebApi()
		{
			var result = Types.InAssembly(PersistenceAssembly)
							  .Should()
							  .NotHaveDependencyOn("Mapdoon.WebApi")
							  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}
	}
}
