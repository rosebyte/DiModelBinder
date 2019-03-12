using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using RoseByte.DiModelBinder.Attributes;

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
				if (context.Metadata.BindingSource != null)
				{
					return null;
				}

				var modelHasContainerServiceAttribute = context.Metadata.ModelType
					.GetCustomAttributes(typeof(ContainerServiceAttribute), true)
					.Any();

				if (!modelHasContainerServiceAttribute)
				{
					return null;
				}
			}

			return new ModelBinder(
				context.Metadata.Properties.ToDictionary(x => x, context.CreateBinder),
				context.Services.GetService(typeof(ILoggerFactory)) as ILoggerFactory);
		}
	}
}