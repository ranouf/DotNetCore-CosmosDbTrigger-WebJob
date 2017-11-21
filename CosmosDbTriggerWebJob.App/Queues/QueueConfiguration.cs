using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDbTriggerWebJob.App.Queues
{
    public static class QueueConfiguration
	{
		// How to find the Queue Name:
		// - open the App Function,
		// - select the Azure Function,
		// - then Integrate,
		// - then Output.
		public const string QUEUE_NAME = "outqueue";
	}
}
