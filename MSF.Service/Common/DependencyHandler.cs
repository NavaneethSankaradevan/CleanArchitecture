using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSF.Persistence;

namespace MSF.Service
{
    public static class DependencyHandler
    {
        private static bool isConfigured;

        public static void ConfigureServices(this IServiceCollection services, IConfiguration config, bool isDev)
        {

            if (isConfigured) return;


			if (isDev)
			{
				services.ConfigureDataContext(
					userDb => userDb.UseInMemoryDatabase("UserDb"),
					tranDb => tranDb.UseInMemoryDatabase("TranDb"));
			}
			else
			{
				// Resolve DbContext dependencies.
				services.ConfigureDataContext(
					user => user.UseSqlServer(config.GetConnectionString("LoginConnection")),
					tran => tran.UseSqlServer(config.GetConnectionString("TranDbConnection")));
			}


			//Note: Assembly.LoadFrom won't work here. It won't throw any error on coding. But dependencies are not resolved.
			Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Assembly.GetExecutingAssembly().Location);

            foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service", StringComparison.InvariantCulture)))
                services.Add(new ServiceDescriptor(type.GetInterfaces().First(i => i.Name.EndsWith(type.Name, StringComparison.InvariantCulture)), type, ServiceLifetime.Scoped));

            isConfigured = true;

        }

    }
}
