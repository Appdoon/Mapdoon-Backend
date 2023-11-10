using Appdoon.Presistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OU_API;
using Respawn;
using Respawn.Graph;
using Appdoon.Domain.Commons;

namespace Mapdoon.Application.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        public static IServiceScopeFactory? _scopeFactory;
        private static string _connectionString;
        private static Respawner _respawner;
        private static DatabaseContext _databaseContext;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTestsAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();
            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.ApplicationName == "Mapdoon.WebApi" &&
            w.EnvironmentName == "Development"));

            startup.ConfigureServices(services);
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _connectionString = _configuration.GetConnectionString("OUAppCon");

            _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
            {
                TablesToIgnore = new Table[]{ "__EFMigrationsHistory" },
            });
        }

        public static async Task ResetStateAsync()
        {
            await _respawner.ResetAsync(_connectionString);
        }

        public static TEntity FindEntity<TEntity>(int id) where TEntity : BaseEntity
        {
            var databaseContext = GetDatabaseContext();
            return databaseContext.Find<TEntity>(id);
        }

        public static int AddEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var databaseContext = GetDatabaseContext();
            databaseContext.Add(entity);
            databaseContext.SaveChanges();
            return entity.Id;
        }

        public static DatabaseContext GetDatabaseContext()
        {
            if(_databaseContext != null)
            {
                return _databaseContext;
            }

            var scope = _scopeFactory.CreateScope();
            _databaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
            return _databaseContext;
        }
    }
}
