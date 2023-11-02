using Appdoon.Application.Services.Users.Command.ForgetPasswordUserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mapdoon.WebApi.OptionsSetup
{
	public class ForgetPasswordOptionsSetup : IConfigureOptions<ForgetPasswordOptions>
	{
		private const string SectionName = "ForgetPassword";
		private readonly IConfiguration _configuration;

		public ForgetPasswordOptionsSetup(IConfiguration configuration)
        {
			_configuration = configuration;

		}
        public void Configure(ForgetPasswordOptions options)
		{
			_configuration.GetSection(SectionName).Bind(options);
		}
	}
}
