using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
	/// <summary>
	/// Marks parameter to be bind with constrictor DI injection.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public class WithDiAttribute : Attribute, IBindingSourceMetadata
	{
		public BindingSource BindingSource { get; } = new BindingSource(
			nameof(WithDiAttribute),
			nameof(WithDiAttribute),
			false,
			true);
	}
}