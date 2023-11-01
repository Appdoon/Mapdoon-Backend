using Mapdoon.Presistence.Features.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mapdoon.WebApi.OptionsSetup
{
	public class EmailOptionsSetup : IConfigureOptions<EmailSettings>
	{
		private const string SectionName = "EmailSettings";
		private readonly IConfiguration _configuration;

		public EmailOptionsSetup(IConfiguration configuration)
        {
			_configuration = configuration;

		}
        public void Configure(EmailSettings options)
		{
			_configuration.GetSection(SectionName).Bind(options);
		}
	}
}
