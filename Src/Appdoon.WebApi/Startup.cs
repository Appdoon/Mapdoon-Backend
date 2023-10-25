using Appdoon.Application.Services.Users.Command.RegisterUserService;
using Appdoon.Application.Validatores.UserValidatore;

using Appdoon.Application.Services.Rate.Command.CreateRateService;

using Appdoon.Common.UserRoles;
using Appdoon.Presistence.Contexts;
using FluentValidation;
using Mapdoon.Application;
using Mapdoon.Common.Interfaces;
using Mapdoon.Domain;
using Mapdoon.Presistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Security.Claims;

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
                    builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS", "PUT")
                    .WithHeaders(
                        HeaderNames.Accept,
                        HeaderNames.ContentType,
                        HeaderNames.Authorization)
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrWhiteSpace(origin)) return false;
                        // Only add this to allow testing with localhost, remove this line in production!  
                        if (origin.ToLower().StartsWith("http://localhost")) return true;
                        // Insert your production domain here.  
                        if (origin.ToLower().StartsWith("https://dev.mydomain.com")) return true;
                        return false;
                    });
                })
            );

            //JSON Serializer
            services.AddControllersWithViews().
                AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());


            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                // Set correct path
                options.LoginPath = new PathString("/api/Authentication/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(500.0);
                options.Cookie.Name = "Appdoon_Auth";
                options.Cookie.HttpOnly = false;
                //new 
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
                options.AddPolicy("Profile", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.User.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
                options.AddPolicy("Teacher", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Teacher.ToString(), UserRole.Admin.ToString()));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString()));
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OU_API", Version = "v1" });
            });


            // Dependecy Injection for All services which inherit from ITransientService
            services.Scan(scan => scan.FromAssembliesOf(typeof(DomainAssembly), typeof(ApplicationAssembly), typeof(PersistanceAssembly), typeof(Program))
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            // Dependecy Injection for All services which inherit from IScopedService
            services.Scan(scan => scan.FromAssembliesOf(typeof(DomainAssembly), typeof(ApplicationAssembly), typeof(PersistanceAssembly), typeof(Program))
                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // create rate for roadmaps service
            services.AddScoped<ICreateRateService, CreateRateService>();

            // Injection for user validatore
            // Be aware of UserValidatore class in Asp.Net
            services.AddScoped<IValidator<RequestRegisterUserDto>, UserValidatore>();

            // Add EF Core
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(option => option.UseSqlServer(Configuration["ConnectionStrings:OUAppCon"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors("Dev");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OU_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                "Photos")),
                RequestPath = "/Photos"
            });
        }
    }
}
