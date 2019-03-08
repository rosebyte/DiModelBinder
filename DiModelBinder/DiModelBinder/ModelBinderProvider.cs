using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace DiModelBinder
{
	public class ModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.BindingSource.Id != WithDependenciesAttribute.Name)
			{
				return null;
			}

			if (!context.Metadata.IsCollectionType &&
			    (context.Metadata.ModelType.GetTypeInfo().IsInterface ||
			     context.Metadata.ModelType.GetTypeInfo().IsAbstract) &&
			    (context.BindingInfo.BindingSource == null ||
			     !context.BindingInfo.BindingSource
				     .CanAcceptDataFrom(BindingSource.Services)))
			{
				var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
				foreach (var property in context.Metadata.Properties)
				{
					propertyBinders.Add(property, context.CreateBinder(property));
				}

				var factory = (ILoggerFactory) context.Services.GetService(typeof(ILoggerFactory));

				return new DiComplexBinder(propertyBinders, factory);
			}

			return null;
		}
	}
}
