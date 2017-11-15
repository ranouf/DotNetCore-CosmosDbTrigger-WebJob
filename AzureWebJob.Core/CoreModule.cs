using Autofac;
using AzureWebJob.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AzureWebJob.Core
{
    public class CoreModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var core = typeof(CoreModule).GetTypeInfo().Assembly;

			builder.RegisterAssemblyTypes(core)
				   .AssignableTo<IDomainService>()
				   .AsImplementedInterfaces()
				   .InstancePerLifetimeScope();

		}
	}
}
