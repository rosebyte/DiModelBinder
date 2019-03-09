using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

			if (context.Metadata.BindingSource.Id != WithDiAttribute.Name)
			{
				return null;
			}

			var propertyBinders = context.Metadata.Properties.ToDictionary(x => x, context.CreateBinder);
			var factory = (ILoggerFactory)context.Services.GetService(typeof(ILoggerFactory));
			var resolver = context.Services.GetService(typeof(DiDependenciesResolver)) as DiDependenciesResolver;

			return new DiComplexBinder(propertyBinders, factory, resolver);
		}
	}
}
