using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.Tests.TestObjects
{
	public class TestModel : ITestModel, INonExistingCls, INonExisting
	{
		[FromQuery]
		public string Name { get; set; } = "TestName";
	}
}
