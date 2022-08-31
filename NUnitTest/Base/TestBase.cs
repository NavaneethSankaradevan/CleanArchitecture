using System;
using Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSF.Persistence;
using MSF.Service;
using NUnit.Framework;

namespace MSF.UnitTest
{

    public abstract class TestBase
	{
		private IServiceProvider _serviceProvider;

		protected IUnitOfWork UnitOfWork { get; private set; }

		
		public void Setup()
		{
			
			var config = new ConfigurationBuilder().SetBasePath(TestContext.CurrentContext.TestDirectory).AddJsonFile("appsettings.json",  true).Build();

			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<IConfiguration>(config);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.ConfigureServices(config,true);
			services.AddLogging();

			_serviceProvider = services.BuildServiceProvider();
			UnitOfWork = _serviceProvider.GetService<IUnitOfWork>();

		}

		protected T GetService<T>()
		{
			return _serviceProvider.GetService<T>();
		}
	}
}