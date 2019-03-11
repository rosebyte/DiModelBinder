using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BindingDictionary = System.Collections.Generic.IDictionary<
	Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata, 
	Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder>;

namespace RoseByte.DiModelBinder
{
	public class DiBinder : ComplexTypeModelBinder
	{
		public DiBinder(BindingDictionary binders, ILoggerFactory logger) : base(binders, logger)
		{ }

		protected override object CreateModel(ModelBindingContext context)
		{
			return context.HttpContext.RequestServices.GetService(context.ModelType);
		}
	}
}
