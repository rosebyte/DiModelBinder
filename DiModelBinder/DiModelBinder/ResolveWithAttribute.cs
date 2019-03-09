using System;

namespace Microsoft.AspNetCore.Mvc
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Class)]
	public class ResolveWithAttribute : Attribute
	{
		public Type Type;

		public ResolveWithAttribute(Type type)
		{
			Type = type;
		}
	}
}
