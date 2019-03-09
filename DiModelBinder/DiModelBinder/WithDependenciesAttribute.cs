using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public class WithDiAttribute : Attribute, IBindingSourceMetadata
	{
		public BindingSource BindingSource => new BindingSource(Name, Name, false, true);

		public const string Name = nameof(WithDiAttribute);
	}
}
