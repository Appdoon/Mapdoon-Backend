using Mapdoon.Application;
using Mapdoon.Domain;
using Mapdoon.Presistence;
using OU_API;
using System.Reflection;

namespace Mapdoon.Architecture.Tests
{
	public class BaseTest
	{
		protected readonly Assembly DomainAssembly = typeof(DomainAssembly).Assembly;
		protected readonly Assembly ApplicationAssembly = typeof(ApplicationAssembly).Assembly;
		protected readonly Assembly PersistenceAssembly = typeof(PersistenceAssembly).Assembly;
		protected readonly Assembly WebApiAssembly = typeof(Program).Assembly;
	}
}
