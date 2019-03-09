using System;

namespace DiModelBinder.IntegrationTests
{
	public class MyService : IMyService
	{
		public string FormatInputs(int id, DateTime created, bool readOnly = false)
		{
			return $"ID: {id} | Created: {created} | ReadOnly {readOnly}";
		}
	}
}
