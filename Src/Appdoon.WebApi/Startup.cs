using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Users.Command.RegisterUserService;
using Appdoon.Application.Validatores.UserValidatore;
using Appdoon.Presistence.Contexts;
using FluentValidation;
using Mapdoon.Application;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Mapdoon.Common;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.User;
using Mapdoon.Domain;
using Mapdoon.Presistence;
using Mapdoon.Presistence.Features.Email;
using Mapdoon.WebApi.Application.Dependencies;
using Mapdoon.WebApi.OptionsSetup;
using Mapdoon.WebApi.WebSocket.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OU_API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//var config = new ConfigurationBuilder()
			//	.AddJsonFile("appsettings.json", optional: false)
			//	.Build();

			var appSettingPath = Environment.GetEnvironmentVariable("ENVIRONMENT_PATH");
			var config = new ConfigurationBuilder()
				.AddJsonFile(appSettingPath ?? "appsettings.json", optional: false)
				.Build();

			var jwtOptions = new JWTOptions();
			config.GetSection("JWTOptions").Bind(jwtOptions);

			var frontDomain = config.GetSection("FrontDomain").Value;

			services.AddHttpContextAccessor();

			services.AddSignalR();

			//services.AddHostedService<AutoMigrateHosted>();
			//services.AddHostedService<BackgroundMigration>();

			//Enable CORS
			// i add allow credentials
			services.AddCors(c =>
			{
				c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});


			services.AddCors(options =>
				options.AddPolicy("Dev", builder =>
				{
					// Allow multiple methods  
					builder
					.AllowAnyMethod()
					//.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS", "PUT")
					//.WithHeaders(
					//	HeaderNames.Accept,
					//	HeaderNames.ContentType,
					//	HeaderNames.Authorization)
					.AllowAnyHeader()
					.AllowCredentials()
					.SetIsOriginAllowed(origin =>
					{
						if(string.IsNullOrWhiteSpace(origin)) return true;
						// Only add this to allow testing with localhost, remove this line in production!  
						if(origin.ToLower().StartsWith("http://localhost")) return true;
						// Insert your production domain here.  
						if(origin.ToLower().StartsWith(frontDomain)) return true;
						if(origin.ToLower().StartsWith("https://dev.mydomain.com")) return true;
						return true;
					});
				})
			);

			//JSON Serializer
			services.AddControllersWithViews().
				AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
				.AddNewtonsoftJson(options =>
				options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());


			// Authentication With Cookie
			//services.AddAuthentication(options =>
			//{
			//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//}).AddCookie(options =>
			//{
			//    // Set correct path
			//    options.LoginPath = new PathString("/api/Authentication/Login");
			//    options.ExpireTimeSpan = TimeSpan.FromMinutes(500.0);
			//    options.Cookie.Name = "Appdoon_Auth";
			//    options.Cookie.HttpOnly = false;
			//    //new 
			//    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
			//});

			// Authentication With JWT
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
								  {
									  options.TokenValidationParameters = new()
									  {
										  ValidateIssuer = true,
										  ValidateAudience = true,
										  ValidateLifetime = true,
										  ValidateIssuerSigningKey = true,
										  ValidIssuer = jwtOptions.Issuer,
										  ValidAudience = jwtOptions.Audience,
										  IssuerSigningKey = new SymmetricSecurityKey(
																  Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
									  };
									  options.Events = new JwtBearerEvents
									  {
										  OnMessageReceived = context =>
										  {
											  var accessToken = context.Request.Query["access_token"];
											  var path = context.HttpContext.Request.Path;
											  if(string.IsNullOrEmpty(accessToken) == false && path.StartsWithSegments("/api_hub"))
											  {
												  // Read the token out of the query string
												  context.Token = accessToken;
											  }
											  return Task.CompletedTask;
										  }
									  };
								  }
								);

			services.ConfigureOptions<JWTOptionsSetup>();
			services.ConfigureOptions<JwtBearerOptionsSetup>();
			services.ConfigureOptions<EmailOptionsSetup>();
			services.ConfigureOptions<ForgetPasswordOptionsSetup>();
			services.ConfigureOptions<RabbitMQOptionsSetup>();

			// Authorization policies with Cookie
			//services.AddAuthorization(options =>
			//{
			//    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
			//    options.AddPolicy("Profile", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
			//    options.AddPolicy("Teacher", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
			//    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString()));
			//});

			//Authorization with JWT
			services.AddAuthorization(options =>
			{
				options.AddPolicy("User", policy => policy.RequireClaim(nameof(UserRole), UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
				options.AddPolicy("Profile", policy => policy.RequireClaim(nameof(UserRole), UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
				options.AddPolicy("Teacher", policy => policy.RequireClaim(nameof(UserRole), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
				options.AddPolicy("Admin", policy => policy.RequireClaim(nameof(UserRole), UserRole.Admin.ToString()));
			});


			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "OU_API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});


			// Dependecy Injection for All services which inherit from ITransientService
			services.Scan(scan => scan.FromAssembliesOf(typeof(DomainAssembly), typeof(ApplicationAssembly), typeof(PersistenceAssembly), typeof(Program), typeof(CommonAssembly))
				.AddClasses(classes => classes.AssignableTo<ITransientService>())
				.AsImplementedInterfaces()
				.WithTransientLifetime());

			// Dependecy Injection for All services which inherit from IScopedService
			services.Scan(scan => scan.FromAssembliesOf(typeof(DomainAssembly), typeof(ApplicationAssembly), typeof(PersistenceAssembly), typeof(Program), typeof(CommonAssembly))
				.AddClasses(classes => classes.AssignableTo<IScopedService>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());

			var emailSettings = new EmailSettings();
			config.GetSection("EmailSettings").Bind(emailSettings);
			services.AddFluentEmail(emailSettings);

			// Dependency Injection for Database Context
			//services.AddScoped<IDatabaseContext, DatabaseContext>();

			// Injection for user validatore
			// Be aware of UserValidatore class in Asp.Net
			services.AddScoped<IValidator<RequestRegisterUserDto>, UserValidatore>();

			var rabbitMQOption = new RabbitMQOption();
			config.GetSection("RabbitMq").Bind(rabbitMQOption);
			services.AddMapdoonMassTransit(Configuration, rabbitMQOption, new[] { Assembly.GetAssembly(typeof(ApplicationAssembly)) });

			// Add EF Core
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<DatabaseContext>(option => option.UseSqlServer(config["ConnectionStrings:OUAppCon"]));

			var dbContext = services.BuildServiceProvider().GetService<IDatabaseContext>();
			dbContext.Database.MigrateAsync();
			Log.Information("Database is connected");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSerilogRequestLogging(configure =>
			{
				configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
			}); // We want to log all HTTP requests

			//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseCors("Dev");

			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OU_API v1"));
				app.UseHttpsRedirection();

				app.UseRouting();

				app.UseAuthentication();
				app.UseAuthorization();

				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
					endpoints.MapHub<MapdoonHub>("/api_hub");
				});

				//app.ma<MapdoonHub>("/Hub");


				app.UseStaticFiles(new StaticFileOptions
				{
					FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
					"Photos")),
					RequestPath = "/Photos"
				});
			}
		}
	}
}
