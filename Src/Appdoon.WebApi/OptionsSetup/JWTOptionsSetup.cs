using Mapdoon.Application.Services.JWTAuthentication.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mapdoon.WebApi.OptionsSetup
{
	public class JWTOptionsSetup : IConfigureOptions<JWTOptions>
	{
		private const string SectionName = "JWTOptions";
		private readonly IConfiguration _configuration;

		public JWTOptionsSetup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void Configure(JWTOptions options)
		{
			_configuration.GetSection(SectionName).Bind(options);
		}
	}
}
