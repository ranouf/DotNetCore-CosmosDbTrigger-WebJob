using AzureWebJob.Core.Samples;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureWebJob.Infrastructure.Samples
{
	public class SampleService : ISampleService
	{
		public async Task<string> SayHello()
		{
			return await Task.Run<string>(() => "Hello");
		}
	}
}
