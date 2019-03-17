using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace RoseByte.DiModelBinder
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
	public class DiClientAttribute : Attribute, IBindingSourceMetadata
	{
		private static readonly BindingSource BindingSourceInstance = new BindingSource(
			nameof(DiClientAttribute),
			nameof(DiClientAttribute),
			false,
			true);

		public ServiceLifetime Lifetime { get; set; }
		public BindingSource BindingSource => BindingSourceInstance;

		public DiClientAttribute() : this(ServiceLifetime.Transient) { }
		public DiClientAttribute(ServiceLifetime lifetime) => Lifetime = lifetime;
	}
}
