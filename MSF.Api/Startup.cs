using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSF.Service;
using MSF.Persistence;
using System.Text;
using Microsoft.OpenApi.Models;

namespace MSF.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment env;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			this.env = env;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			ConfigureAuthentication(services);

			// Resolve the service dependencies.
			services.ConfigureServices(Configuration,env.IsDevelopment());

			// Below is required to identity (signinmanager) services to work. 
			// Since ILogger is no longer registered by default but ILogger<T> is.
			services.AddLogging(l => l.AddConsole());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});

			services.AddControllers();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{

			//Configure cross origin request handler
			app.UseCors(builder =>
			{
				builder.AllowAnyHeader();
				builder.AllowCredentials();

				// Enable request for specific origin (ULR address) based on config file.
				builder.WithOrigins(Configuration.GetSection("AllowdOrigins").Get<string[]>());

				// Enable for specific type (GET,POST) based on config file.
				builder.WithMethods(Configuration.GetSection("AlloudMethods").Get<string[]>());

			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		/// <summary>
		/// Configure Authentication and Authorization
		/// </summary>
		/// <param name="services"></param>
		private void ConfigureAuthentication(IServiceCollection services)
		{
			var issuers = Configuration.GetSection("Jwt:Issuers").Get<string[]>();

			// JWT token authentication.
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = Configuration["Jwt:Issuer"],
					ValidAudience = Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
					ClockSkew = System.TimeSpan.Zero // Remove delay of token when expire                
				};
			});

			// Role based policy authorization.
			services.AddSingleton<IAuthorizationPolicyProvider, RoleBasedPolicyProvider>();
			services.AddAuthorization();

		}
	}
}
