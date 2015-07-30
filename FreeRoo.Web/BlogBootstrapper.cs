using System;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Autofac;
using System.Reflection;

namespace FreeRoo.Web
{
	public class BlogBootstrapper:AutofacNancyBootstrapper
	{
		protected override void ApplicationStartup (ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup (container, pipelines);
			StaticConfiguration.DisableErrorTraces = false;
		}
		protected override void ConfigureApplicationContainer (ILifetimeScope existingContainer)
		{
			var builder = new ContainerBuilder ();
			builder.RegisterType<Logger> ().As<ILogger> ().SingleInstance ();
			builder.RegisterAssemblyTypes (Assembly.GetExecutingAssembly ())
				.Where (t => typeof(IDependency).IsAssignableFrom (t) && t.Name.EndsWith ("Service"))
				.AsImplementedInterfaces ();
			builder.RegisterGeneric (typeof(Repository<>))
				.As (typeof(IRepository<>)).InstancePerDependency ();
			builder.Update (existingContainer.ComponentRegistry);
		}
	}
}

