using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace RoseByte.DiModelBinder.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContainerServiceAttribute : Attribute
	{
		public ServiceLifetime Lifetime { get; set; }

		public ContainerServiceAttribute() : this(ServiceLifetime.Transient) { }
		public ContainerServiceAttribute(ServiceLifetime lifetime) => Lifetime = lifetime;
	}
}
