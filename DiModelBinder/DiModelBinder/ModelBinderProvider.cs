using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace DiModelBinder
{
	public class ModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.BindingSource?.Id != nameof(WithDiAttribute))
			{
				return null;
			}

			var propertyBinders = context.Metadata.Properties.ToDictionary(x => x, context.CreateBinder);

			if (!(context.Services.GetService(typeof(ILoggerFactory)) is ILoggerFactory factory))
			{
				throw new Exception("Missing factory");
			}

			if (!(context.Services.GetService(typeof(IDiResolver)) is IDiResolver resolver))
			{
				throw new Exception("Missing resolver");
			}

			return new DiBinder(propertyBinders, factory, resolver);
		}
	}
}
