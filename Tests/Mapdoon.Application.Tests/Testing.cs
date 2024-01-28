using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Users;
using Appdoon.Presistence.Contexts;
using Mapdoon.Presistence.Features.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OU_API;
using Respawn;
using Respawn.Graph;

namespace Mapdoon.Application.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        public static IServiceScopeFactory? _scopeFactory;
        private static string _connectionString;
        private static Respawner _respawner;
        private static DatabaseContext? _databaseContext;
        private static FacadeFileHandler? _facadeFileHandler;

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
                TablesToIgnore = new Table[] { "__EFMigrationsHistory" },
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

        public static int AddUser()
        {
            var userId = AddEntity(new User
            {
                Email = "test@gmail.com",
                Password = "password",
            });

            return userId;
        }

        public static DatabaseContext GetDatabaseContext()
        {
            if (_databaseContext != null)
            {
                return _databaseContext;
            }

            var scope = _scopeFactory.CreateScope();
            _databaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
            return _databaseContext;
        }

        public static FacadeFileHandler GetFacadeFileHandler()
        {
            if (_facadeFileHandler != null)
            {
                return _facadeFileHandler;
            }

            var scope = _scopeFactory.CreateScope();
            _facadeFileHandler = scope.ServiceProvider.GetService<FacadeFileHandler>();
            return _facadeFileHandler;
        }

        public static void ResetDatabaseContext()
        {
            _databaseContext = null;
        }

        public static void ResetFacadeFileHandler()
        {
            _facadeFileHandler = null;
        }
    }
}
