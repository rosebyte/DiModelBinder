using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.Tests.TestObjects
{
	[ResolveWith(typeof(TestModel))]
	public interface INonExistingCls
	{
		
	}
}