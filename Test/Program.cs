using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
		{
			TelemetryConfiguration.Active.InstrumentationKey =
				"fbb7fe05-d43d-4ed2-bfe8-275f13d76408";
			TelemetryClient client = new TelemetryClient();
			client.TrackTrace("Demo application starting up.");

			for (int i = 0; i < 10; i++)
			{
				client.TrackEvent("Testing " + i);
			}

			client.TrackException(new Exception("Demo exception."));
			client.TrackTrace("Demo application exiting.");
			client.Flush();
			Thread.Sleep(5000);
			Console.ReadLine();
		}
    }
}
