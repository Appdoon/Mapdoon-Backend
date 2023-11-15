using Appdoon.Application.Interfaces;
using Appdoon.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.Application.HostedServices
{
	public class AutoMigrateHosted : IHostedService
	{
		private readonly IDatabaseContext _databaseContext;

		public AutoMigrateHosted(IDatabaseContext databaseContext)
        {
			_databaseContext = databaseContext;

		}
        public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _databaseContext.Database.MigrateAsync();
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await Task.CompletedTask;
		}
	}
}
