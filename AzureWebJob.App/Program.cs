using AzureWebJob.Core.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace AzureWebJob.App
{
    public static class Program
	{
		public static void Main(string[] args)
		{
			var startup = new Startup();
			IServiceProvider serviceProvider = startup.ConfigureServices(new ServiceCollection());
			serviceProvider.AddHandlerException();
			serviceProvider.GetService<App>().RunAsync().Wait();
		}

		public static void AddHandlerException(this IServiceProvider serviceProvider)
		{
			var applicationInsightsSettings = serviceProvider
				.GetService<IOptions<ApplicationInsightsSettings>>()
				.Value;

			var telemetryClient = serviceProvider.GetService<TelemetryClient>();
			telemetryClient.Context.InstrumentationKey = applicationInsightsSettings.InstrumentationKey;

			AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
			{
				var e = eventArgs.ExceptionObject as Exception;
				telemetryClient.TrackException(e.GetBaseException());
				telemetryClient.Flush();
			};
		}
	}
}
