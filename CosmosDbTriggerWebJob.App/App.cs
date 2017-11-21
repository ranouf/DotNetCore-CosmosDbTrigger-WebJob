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
	public class App :IDisposable
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

		public void Dispose()
		{
			_telemetryClient.Flush();
		}

		// IMPORTANT: Please verify that your Queue Name match with your Azure configuration
		public async Task DoSomethingOnAQueue([QueueTrigger(QueueConfiguration.QUEUE_NAME)] string notification, TextWriter textWriter)
		{
			_telemetryClient.TrackTrace("Start DoSomethingOnAQueue");
			Console.WriteLine(notification);
			_telemetryClient.TrackTrace("End DoSomethingOnAQueue");
		}
	}
}
