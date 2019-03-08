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
		public DiComplexBinder(BindingDictionary propertyBinder, ILoggerFactory logger)
			: base(propertyBinder, logger) { }

		protected override object CreateModel(ModelBindingContext bindingContext) => 
			bindingContext.HttpContext.RequestServices.GetService(bindingContext.ModelType);
	}
}
