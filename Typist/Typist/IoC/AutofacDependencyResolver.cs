using Autofac;

namespace Typist.IoC
{
	public class AutofacDependencyResolver : IDepedencyResolver
	{
		private readonly IContainer _container;

		public AutofacDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public T Get<T>() => _container.Resolve<T>();
	}
}