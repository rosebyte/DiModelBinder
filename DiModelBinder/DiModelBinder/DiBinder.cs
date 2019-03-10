using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using BindingDictionary = System.Collections.Generic.IDictionary<
	Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata, 
	Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder>;

namespace DiModelBinder
{
	public class DiBinder : ComplexTypeModelBinder
	{
		private readonly IDiResolver _resolver;

		public DiBinder(BindingDictionary binders, ILoggerFactory logger, IDiResolver resolver) 
			: base(binders, logger)
		{
			_resolver = resolver;
		}

		protected override object CreateModel(ModelBindingContext context)
		{
			return _resolver.ResolveModel(context.ModelType, context.HttpContext.RequestServices);
		}
	}
}
