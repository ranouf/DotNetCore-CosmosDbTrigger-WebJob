using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTriggerWebJob.Core.Samples
{
	public class SampleManager : ISampleManager
	{
		private readonly ISampleService _samplesService;

		public SampleManager(ISampleService samplesService)
		{
			_samplesService = samplesService;
		}

		public async Task<string> SayHello()
		{
			return await _samplesService.SayHello();
		}
	}
}
