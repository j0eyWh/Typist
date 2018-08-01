namespace Typist.IoC
{
	public interface IDepedencyResolver
	{
		/// <summary>
		/// Resolves a service of type <see cref="T"/>
		/// </summary>
		/// <typeparam name="T">Service to be resolved</typeparam>
		/// <returns>An instance of the requested service</returns>
		T Get<T>();
	}
}