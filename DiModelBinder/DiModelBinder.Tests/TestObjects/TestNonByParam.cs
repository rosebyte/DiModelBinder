using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.Tests.TestObjects
{
	public class TestNonByParam
	{
		private object _model;

		public TestNonByParam([ResolveWith(typeof(TestModel))]INonExisting model)
		{
			_model = model;
		}
	}
}
