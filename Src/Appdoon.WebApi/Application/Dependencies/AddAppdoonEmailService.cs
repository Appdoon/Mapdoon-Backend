using FluentEmail.MailKitSmtp;
using Mapdoon.Presistence.Features.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mapdoon.WebApi.Application.Dependencies
{
	public static class AddAppdoonEmailService
	{
		public static void AddFluentEmail(this IServiceCollection services, EmailSettings emailSettings)
		{
			var defaultFromEmail = emailSettings.DefaultFromEmail;
			var host = emailSettings.Host;
			var port = emailSettings.Port;
			var userName = emailSettings.UserName;
			var password = emailSettings.Password;

			services.AddFluentEmail("mapdoooon@gmail.com")
				 //   .AddMailKitSender(new SmtpClientOptions()
					//{
					//	User = userName,
					//	Port = 25,
					//	Password = "Mapdoon123!",
					//	Server = "localhost",
					//	UseSsl = false,
					//})
				    .AddSmtpSender("smtp.mailtrap.io", 587, userName, password);
		}
	}
}
