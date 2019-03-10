using System;

namespace Microsoft.AspNetCore.Mvc
{
	/// <summary>
	/// Helps resolver to determine implementation class for given interface or
	/// abstract class
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Interface)]
	public class ResolveWithAttribute : Attribute
	{
		public Type Type;

		/// <summary>
		/// Constructs an instance of <see cref="ResolveWithAttribute"/>.
		/// </summary>
		/// <param name="type">Type implementing given interface or abstract class</param>
		public ResolveWithAttribute(Type type) => Type = type;
	}
}
