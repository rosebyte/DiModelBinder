using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiModelBinder
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
		/// Adds resolver to resolve model types dynamically
		/// </summary>
		/// <param name="services">IServiceCollection in application Startup class</param>
		public static void AddModelBindingDiResolver(this IServiceCollection services) => 
			services.AddSingleton<IDiResolver, DiResolver>();
	}
}
