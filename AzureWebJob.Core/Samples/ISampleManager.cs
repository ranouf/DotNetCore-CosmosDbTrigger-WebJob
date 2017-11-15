using AzureWebJob.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureWebJob.Core.Samples
{
    public interface ISampleManager : IDomainService
	{
		Task<string> SayHello();
    }
}
