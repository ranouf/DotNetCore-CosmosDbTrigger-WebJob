using AzureWebJob.Core.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading;

namespace AzureWebJob.App
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var startup = new Startup();
			IServiceProvider serviceProvider = startup.ConfigureServices(new ServiceCollection());
			startup.Configure(serviceProvider);
			serviceProvider.GetService<App>().RunAsync().Wait();
		}
	}
}
