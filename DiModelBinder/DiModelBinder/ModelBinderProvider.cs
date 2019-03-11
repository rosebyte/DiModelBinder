using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace RoseByte.DiModelBinder
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
			var factory = context.Services.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
			
			return new DiBinder(propertyBinders, factory);
		}
	}
}
