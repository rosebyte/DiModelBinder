using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.DiModelBinder;
using RoseByte.DiModelBinder.Attributes;

namespace Microsoft.AspNetCore.Mvc
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
		/// Registers all types decorated with ContainerService family attributes
		/// </summary>
		/// <param name="services">IServiceCollection instance in Startup</param>
		/// <param name="assemblies">Assemblies to scan, default is EntryAssembly</param>
		public static void RegisterDiTypes(this IServiceCollection services, params Assembly[] assemblies)
		{
			if (!assemblies.Any())
			{
				assemblies = new[] {Assembly.GetEntryAssembly(), Assembly.GetCallingAssembly()};
			}

			var types = assemblies
				.SelectMany(x => x.GetTypes())
				.Where(x => x.GetCustomAttributes(typeof(ContainerServiceAttribute), false).Any())
				.Distinct();

			foreach (var type in types)
			{
				var attribute = type
					.GetCustomAttributes(typeof(ContainerServiceAttribute), false)
					.Single();

				if (attribute is ContainerServiceAttribute service)
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
