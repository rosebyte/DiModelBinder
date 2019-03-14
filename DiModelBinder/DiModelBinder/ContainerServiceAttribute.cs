using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace RoseByte.DiModelBinder.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
	public class DiClientAttribute : Attribute, IBindingSourceMetadata
	{
		private static BindingSource _bindingSource = new BindingSource(
			nameof(DiClientAttribute),
			nameof(DiClientAttribute),
			false,
			true);

		public ServiceLifetime Lifetime { get; set; }
		public BindingSource BindingSource => _bindingSource;

		public DiClientAttribute() : this(ServiceLifetime.Transient) { }
		public DiClientAttribute(ServiceLifetime lifetime) => Lifetime = lifetime;
	}
}
