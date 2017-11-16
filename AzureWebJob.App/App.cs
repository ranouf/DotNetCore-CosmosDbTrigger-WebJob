using AzureWebJob.Core.Configuration;
using AzureWebJob.Core.Samples;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureWebJob.App
{
	class App
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

		public async Task RunAsync()
		{
			_telemetryClient.TrackTrace("Operations running");
			Console.WriteLine($"ApplicationInsight status:{!string.IsNullOrEmpty(_telemetryClient.InstrumentationKey) }");
			Console.WriteLine($"Sample status:{!string.IsNullOrEmpty(await _sampleManager.SayHello()) }");
			Console.WriteLine($"Configuration status:{!string.IsNullOrEmpty(_sampleSettings.SampleValue) }");
			_telemetryClient.TrackTrace("Operations finished ");

			_telemetryClient.Flush();
			Thread.Sleep(1000); // Need to wait to send tracks to Application Insights
		}
	}
}
