using System;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Marks parameter to be bind with constructor DI injection.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DiTypeAttribute : Attribute
	{
		public Type[] Interfaces { get; }
		public DiTypeAttribute(params Type[] interfaces) => Interfaces = interfaces;
	}
}