using Appdoon.Domain.Commons;
using FluentAssertions;
using NetArchTest.Rules;

namespace Mapdoon.Architecture.Tests.DomainTests
{
	public class DomainTests : BaseTest
	{
		[Fact]
		public void EntitiesShouldInheritFromBaseEntity()
		{
			var result = Types.InAssembly(DomainAssembly)
							  .That()
							  .ResideInNamespaceContaining("Mapdoon.Domain.Entities")
							  .Should()
							  .Inherit(typeof(BaseEntity))
							  .GetResult();

			result.IsSuccessful.Should().BeTrue();
		}

	}
}
