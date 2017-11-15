using Autofac;
using AzureWebJob.Core.Samples;
using AzureWebJob.Infrastructure.Samples;
using System;
using System.Reflection;

namespace AzureWebJob.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
				   .Where(t => t.Name.EndsWith("Service"))
				   .AsImplementedInterfaces();

			builder.RegisterType<SampleService>().As<ISampleService>();
		}
	}
}
