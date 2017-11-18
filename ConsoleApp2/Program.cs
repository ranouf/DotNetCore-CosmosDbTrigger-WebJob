using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile("appsettings.Development.json", optional: true)
				.AddEnvironmentVariables()
				.Build();

			var jobHostConfiguration = new JobHostConfiguration()
			{
				DashboardConnectionString = configuration.GetConnectionString("AzureWebJobsDashboard"),
				StorageConnectionString = configuration.GetConnectionString("AzureWebJobsStorage"),
			};

			var host = new JobHost(jobHostConfiguration);
			//host.CallAsync(typeof(Program).GetMethod("ProcessCandidates"));
			host.RunAndBlock();
		}

		//public static void ProcessCandidates([QueueTrigger("candidate")] object candidate, TextWriter textWriter)
		//{
		//	Console.WriteLine(candidate);
		//}

		public async static Task ProcessMessageAsync([QueueTrigger("candidates")] string message)
		{
			await Task.Delay(50);

			Console.WriteLine("Processing Message Async...");
			Console.WriteLine(message);
		}
	}
}
