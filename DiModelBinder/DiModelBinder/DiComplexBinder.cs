using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using BindingDictionary = System.Collections.Generic.IDictionary<
	Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata, 
	Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder>;

namespace DiModelBinder
{
	public class DiComplexBinder : ComplexTypeModelBinder
	{
		private readonly DiDependenciesResolver _resolver;

		public DiComplexBinder(
			BindingDictionary propertyBinder,
			ILoggerFactory logger,
			DiDependenciesResolver resolver) : base(propertyBinder, logger)
		{
			_resolver = resolver;
		}

		protected override object CreateModel(ModelBindingContext bindingContext)
		{
			return _resolver != null
				? _resolver.ResolveModel(bindingContext.ModelType, bindingContext.HttpContext.RequestServices)
				: bindingContext.HttpContext.RequestServices.GetService(bindingContext.ModelType);
		}
	}
}
