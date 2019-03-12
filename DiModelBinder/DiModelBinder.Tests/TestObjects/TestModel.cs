using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.DiModelBinder.Attributes;

namespace DiModelBinder.Tests.TestObjects
{
	[ContainerService]
	public class TestModel : ITestModel
	{
		[FromQuery]
		public string Name { get; set; } = "TestName";
	}
}
