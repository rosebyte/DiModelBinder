using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RoseByte.DiModelBinder
{
	public static class Configuration
	{
		/// <summary>
		/// Adds model binder provider enabling the whole inject process
		/// </summary>
		/// <param name="options">MvcOptions from commom IServiceCollection.AddMvc(mvcOptions =>...</param>
		public static void InsertDiModelBinderProvider(this MvcOptions options) =>
			options.ModelBinderProviders.Insert(0, new ModelBinderProvider());

		/// <summary>
		/// Registers all types decorated with <see cref="DiClientAttribute"/> attribute
		/// </summary>
		/// <param name="services">IServiceCollection instance in Startup</param>
		/// <param name="assemblies">Assemblies to scan, default is EntryAssembly</param>
		public static void RegisterDiClients(this IServiceCollection services, params Assembly[] assemblies)
		{
			if (!assemblies.Any())
			{
				assemblies = new[] {Assembly.GetEntryAssembly(), Assembly.GetCallingAssembly()};
			}

			var types = assemblies
				.SelectMany(x => x.GetTypes())
				.Where(x => x.GetCustomAttributes(typeof(DiClientAttribute), false).Any())
				.Distinct();

			foreach (var type in types)
			{
				var attribute = type
					.GetCustomAttributes(typeof(DiClientAttribute), false)
					.Single();

				if (attribute is DiClientAttribute service)
				{
					switch (service.Lifetime)
					{
						case ServiceLifetime.Singleton:
							services.AddSingleton(type);
							break;
						case ServiceLifetime.Scoped:
							services.AddScoped(type);
							break;
						case ServiceLifetime.Transient:
							services.AddTransient(type);
							break;
					}
				}
			}
		}
	}
}
