using AzureWebJob.Core.Configuration;
using AzureWebJob.Core.Samples;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureWebJob.App
{
	class App
	{
		private readonly ISampleManager _sampleManager;
		private readonly SampleSettings _appSettings;

		public App(
			ISampleManager sampleManager,
			IOptions<SampleSettings> appSettings
		)
		{
			_sampleManager = sampleManager;
			_appSettings = appSettings.Value;
		}

		public async Task RunAsync()
		{
			Console.WriteLine($"{await _sampleManager.SayHello()} {_appSettings.SampleValue}");
			Console.ReadKey();
		}
	}
}
