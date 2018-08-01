using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Autofac;
using Typist.Services;

namespace Typist.IoC
{
	public class AutofacContainerBootstrapper : IContainerBootStraper
	{
		public IDepedencyResolver GetDepedencyResolver() =>
			new AutofacDependencyResolver(CreateContainer());

		private IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();

			RegisterServices(builder);

			return builder.Build();
		}

		private void RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterType<WordsLoader>();
		}
	}
}
