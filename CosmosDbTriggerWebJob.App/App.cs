using CosmosDbTriggerWebJob.App.Queues;
using CosmosDbTriggerWebJob.Core.Configuration;
using CosmosDbTriggerWebJob.Core.Samples;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosDbTriggerWebJob.App
{
	public class App
	{
		private readonly TelemetryClient _telemetryClient;
		private readonly ISampleManager _sampleManager;
		private readonly SampleSettings _sampleSettings;

		public App(
			TelemetryClient telemetryClient,
			ISampleManager sampleManager,
			IOptions<SampleSettings> sampleSettings
		)
		{
			_telemetryClient = telemetryClient;
			_sampleManager = sampleManager;
			_sampleSettings = sampleSettings.Value;
		}

		public async Task DoSomethingOnAQueue([QueueTrigger(QueueConfiguration.CANDIDATE_QUEUE_NAME)] string notification, TextWriter textWriter)
		{
			Console.WriteLine(notification);
		}

		////public void ProcessNotifications([QueueTrigger(NOTIFICATION_QUEUE_NAME)] string notification, TextWriter textWriter)
		////{
		////	Console.WriteLine(notification);
		////}
		//public void ProcessQueueMessage([QueueTrigger(NOTIFICATION_QUEUE_NAME)] string logMessage, TextWriter logger)
		//{
		//	Console.WriteLine(logMessage);
		//}
	}
}
