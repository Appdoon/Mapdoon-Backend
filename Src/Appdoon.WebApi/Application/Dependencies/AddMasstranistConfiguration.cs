using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.Application.Dependencies
{
	public static class AddMasstranistConfiguration
	{
		public static IServiceCollection AddMapdoonMassTransit(this IServiceCollection services,
			IConfiguration configuration, RabbitMQOption rabbitMQOption, params Assembly[] assemblies)
		{
			rabbitMQOption.Host = Environment.GetEnvironmentVariable("RABIITMQ_HOST") ?? "localhost";
			rabbitMQOption.VirtualPath = Environment.GetEnvironmentVariable("RABIITMQ_VIRTUALPATH") ?? "/";
			rabbitMQOption.Username = Environment.GetEnvironmentVariable("RABIITMQ_USERNAME") ?? "guest";
			rabbitMQOption.Password = Environment.GetEnvironmentVariable("RABIITMQ_PASSWORD") ?? "guest";

			services.AddMassTransit(x =>
			{
				x.AddConsumers(assemblies);

				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host(rabbitMQOption.Host, rabbitMQOption.VirtualPath, h =>
					{
						h.Username(rabbitMQOption.Username);
						h.Password(rabbitMQOption.Password);
						h.Heartbeat(TimeSpan.FromSeconds(5));
					});

					cfg.ConfigureEndpoints(context);

					// remove queuse after restart
					cfg.AutoDelete = true;
				});


				//x.AddRequestClient<AddComponentToMenuStructureEvent>();
			});

			services.AddMassTransitHostedService();


			return services;
		}
	}

	public class RabbitMQOption
	{
		public string Host { get; set; } = "localhost";
		public string VirtualPath { get; set; } = "/";
		public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
	}

}
