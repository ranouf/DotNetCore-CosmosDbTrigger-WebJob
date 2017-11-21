using CosmosDbTriggerWebJob.App.WebJob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CosmosDbTriggerWebJob.App
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var startup = new Startup();
			var serviceProvider = startup.ConfigureServices(new ServiceCollection());
			startup.Configure(serviceProvider);
			
			var jobHostConfiguration = new JobHostConfiguration()
			{
				JobActivator = new JobActivator(serviceProvider),
			};

			var host = new JobHost(jobHostConfiguration);
			host.RunAndBlock();
		}
	}
}
