using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public class WithDependenciesAttribute : Attribute, IBindingSourceMetadata
	{
		public BindingSource BindingSource => new BindingSource(Name, $"{Name}Attribute", false, true);

		public const string Name = "WithDependencies";
	}
}
