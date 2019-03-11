using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiModelBinder.Tests.TestObjects
{
	[DiType(typeof(ITestModel))]
	public class TestModel : ITestModel
	{
		[FromQuery]
		public string Name { get; set; } = "TestName";
	}
}
