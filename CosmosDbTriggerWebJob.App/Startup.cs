﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using CosmosDbTriggerWebJob.Core;
using CosmosDbTriggerWebJob.Core.Configuration;
using CosmosDbTriggerWebJob.Infrastructure;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace CosmosDbTriggerWebJob.App
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
			services.Configure<ApplicationInsightsSettings>(Configuration.GetSection("ApplicationInsights"));
			services.Configure<ConnectionStringsSettings>(Configuration.GetSection("ConnectionStrings"));

			// Application Insights
			services.AddSingleton<TelemetryClient>();

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

		public void Configure(IServiceProvider serviceProvider)
		{
			ConfigureApplicationInsights(serviceProvider);
			ConfigureAzureWebJobs(serviceProvider);
			AddHandlerException(serviceProvider);
		}

		#region Private
		private void ConfigureApplicationInsights(IServiceProvider serviceProvider)
		{
			var applicationInsightsSettings = serviceProvider
				.GetService<IOptions<ApplicationInsightsSettings>>()
				.Value;

			var telemetryClient = serviceProvider.GetService<TelemetryClient>();
			telemetryClient.Context.InstrumentationKey = applicationInsightsSettings.InstrumentationKey;
		}

		private void ConfigureAzureWebJobs(IServiceProvider serviceProvider)
		{
			var connectionStringSettings = serviceProvider
				.GetService<IOptions<ConnectionStringsSettings>>()
				.Value;

			Environment.SetEnvironmentVariable("AzureWebJobsDashboard", connectionStringSettings.AzureWebJobsDashboard);
			Environment.SetEnvironmentVariable("AzureWebJobsStorage", connectionStringSettings.AzureWebJobsStorage);
		}

		public void AddHandlerException(IServiceProvider serviceProvider)
		{
			AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
			{
				var telemetryClient = serviceProvider.GetService<TelemetryClient>();
				var e = eventArgs.ExceptionObject as Exception;
				telemetryClient.TrackException(e.GetBaseException());
				telemetryClient.Flush();
				Thread.Sleep(1000); // Need to wait to send tracks to Application Insights
			};
		}
		#endregion
	}
}
