using Mapdoon.WebApi.Application.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mapdoon.WebApi.OptionsSetup
{
	public class RabbitMQOptionsSetup : IConfigureOptions<RabbitMQOption>
	{
		private const string SectionName = "RabbitMq";
		private readonly IConfiguration _configuration;

		public RabbitMQOptionsSetup(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public void Configure(RabbitMQOption options)
		{
			_configuration.GetSection(SectionName).Bind(options);
		}
	}
}
