using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiModelBinder
{
	public static class Configuration
	{
		public static void AddModelBinderProvider(this MvcOptions options) =>
			options.ModelBinderProviders.Insert(0, new ModelBinderProvider());

		public static void AddModelBindingDiResolver(this IServiceCollection services) => 
			services.AddSingleton<DiDependenciesResolver>();
	}
}
