using FluentEmail.MailKitSmtp;
using Mapdoon.Presistence.Features.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

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

			services.AddFluentEmail(defaultFromEmail)
					.AddSmtpSender(new SmtpClient()
					{
						Host = host,
						Port = port,
						Credentials = new NetworkCredential(userName, password),
						EnableSsl = true,
					});
				    //.AddSmtpSender("smtp.gmail.com", 587, "mapdoooon@gmail.com", "wobhywvsgvzxlirr");
		}
	}
}
