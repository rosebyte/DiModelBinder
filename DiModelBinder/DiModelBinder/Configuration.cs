using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.DiModelBinder;

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
		/// Registers all types decorated with [DiType] attribute
		/// </summary>
		/// <param name="services">IServiceCollection instance in Startup</param>
		public static void RegisterDiTypes(this IServiceCollection services)
		{
			var types = Assembly
				.GetCallingAssembly()
				.GetTypes()
				.Where(x => x.GetCustomAttributes(typeof(DiTypeAttribute), true).Length > 0)
				.ToList();

			foreach (var type in types)
			{
				var attribute = type.GetCustomAttributes(typeof(DiTypeAttribute), true).First();

				if (!(attribute is DiTypeAttribute diAttribute))
				{
					continue;
				}

				services.AddTransient(type);
				
				foreach (var inter in diAttribute.Interfaces)
				{
					services.AddTransient(inter, type);
				}
			}
		}
	}
}
