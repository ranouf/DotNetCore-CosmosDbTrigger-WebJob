using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureWebJob.App
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var startup = new Startup();
			IServiceProvider serviceProvider = startup.ConfigureServices(new ServiceCollection());
			serviceProvider.GetService<App>().RunAsync().Wait();
		}
	}
}
