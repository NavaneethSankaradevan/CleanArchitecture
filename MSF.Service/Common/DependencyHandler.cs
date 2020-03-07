using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using MSF.Application;
using Microsoft.Extensions.Configuration;
using System.Runtime.Loader;
using System;

namespace MSF.Service
{
    public static class DependencyHandler
    {
        private static bool isConfigured;

        public static void ConfigureServices(this IServiceCollection services)
        {

            if (isConfigured) return;

            //Note: Assembly.LoadFrom won't work here. It won't throw any error on coding. But dependencies are not resolved.
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Assembly.GetExecutingAssembly().Location);

            foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service", StringComparison.InvariantCulture)))
                services.Add(new ServiceDescriptor(type.GetInterfaces().First(i => i.Name.EndsWith(type.Name, StringComparison.InvariantCulture)), type, ServiceLifetime.Scoped));

            isConfigured = true;

        }

    }
}
