using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
		static void Main(string[] args)
		{
			IServiceCollection serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			var configuration = new JobHostConfiguration();
			configuration.Queues.MaxPollingInterval = TimeSpan.FromSeconds(10);
			configuration.Queues.BatchSize = 1;
			configuration.JobActivator = new CustomJobActivator(serviceCollection.BuildServiceProvider());
			//configuration.UseTimers();

			var host = new JobHost(configuration);
			host.RunAndBlock();
		}

		private static void ConfigureServices(IServiceCollection serviceCollection)
		{
			// Setup your container here, just like a asp.net core app

			// Optional: Setup your configuration:
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
			serviceCollection.Configure<MySettings>(configuration);

			// A silly example of wiring up some class used by the web job:
			serviceCollection.AddScoped<ISomeInterface, SomeUsefulClass>();
			// Your classes that contain the webjob methods need to be DI-ed up too
			serviceCollection.AddScoped<WebJobsMethods, WebJobsMethods>();

			// One more thing - tell azure where your azure connection strings are			
			Environment.SetEnvironmentVariable("AzureWebJobsDashboard", configuration.GetConnectionString("AzureWebJobsDashboard"));
			Environment.SetEnvironmentVariable("AzureWebJobsStorage", configuration.GetConnectionString("AzureWebJobsStorage"));
		}
	}
	public class MySettings
	{
		public string AzureWebJobsDashboard { get; set; }
		public string AzureWebJobsStorage { get; set; }
	}


	public class CustomJobActivator : IJobActivator
	{
		private readonly IServiceProvider _service;
		public CustomJobActivator(IServiceProvider service)
		{
			_service = service;
		}

		public T CreateInstance<T>()
		{
			var service = _service.GetService<T>();
			return service;
		}
	}
	public class WebJobsMethods // Other class names are available.
	{
		private readonly ISomeInterface _usefulClass;

		public WebJobsMethods(ISomeInterface usefulClass)
		{
			_usefulClass = usefulClass;
		}

		public async Task DoSomethingOnATimer([TimerTrigger("45 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, TextWriter log)
		{
			await _usefulClass.MakeACuppa();
		}

		//public async Task DoSomethingOnAQueue([QueueTrigger("candidates")] int id)
		//{
		//	await _usefulClass.DoSomethingAmazing(id);
		//}

		public async Task DoSomethingOnAQueue([QueueTrigger("candidates")] string notification, TextWriter textWriter)
		{
			await _usefulClass.DoSomethingAmazing(notification);
		}

	}

	public interface ISomeInterface {
		Task DoSomethingAmazing(int id);
		Task DoSomethingAmazing(string notification);
		Task MakeACuppa();
	}

	public class SomeUsefulClass : ISomeInterface
	{
		public async Task DoSomethingAmazing(int id)
		{
			await Task.Run(()=> Console.WriteLine(id));
		}
		public async Task DoSomethingAmazing(string notification)
		{
			await Task.Run(() => Console.WriteLine(notification));
		}

		public async Task MakeACuppa()
		{
			await Task.Run(() => Console.WriteLine("MakeACuppa"));
		}
	}

}
