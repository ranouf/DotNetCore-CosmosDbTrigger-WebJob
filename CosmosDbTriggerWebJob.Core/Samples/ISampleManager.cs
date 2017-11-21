using CosmosDbTriggerWebJob.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTriggerWebJob.Core.Samples
{
    public interface ISampleManager : IDomainService
	{
		Task<string> SayHello();
    }
}
