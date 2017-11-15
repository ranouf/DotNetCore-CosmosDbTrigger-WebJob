using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureWebJob.Core;
using AzureWebJob.Core.Configuration;
using AzureWebJob.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AzureWebJob.App
{
    public class Startup
	{
		public IConfigurationRoot Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }

		public Startup()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true)
				.AddJsonFile("appsettings.development.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// App settings
			services.AddOptions();
			services.Configure<SampleSettings>(Configuration.GetSection("SampleSettings"));

			// Add app
			services.AddTransient<App>();

			#region Autofac
			// Create the container builder.
			var builder = new ContainerBuilder();
			builder.RegisterModule(new CoreModule());
			builder.RegisterModule(new InfrastructureModule());

			builder.Populate(services);
			ApplicationContainer = builder.Build();
			#endregion

			// Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(ApplicationContainer);
		}
	}
}
