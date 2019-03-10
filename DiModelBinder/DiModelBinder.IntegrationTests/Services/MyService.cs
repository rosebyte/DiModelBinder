using System;

namespace DiModelBinder.IntegrationTests
{
	public class MyService : IMyService
	{
		public string FormatInputs(int id, DateTime created, bool? readOnly = null)
		{
			var result = $"ID: {id} | Created: {created:MM/dd/yyyy}";

			if (readOnly.HasValue)
			{
				result += $" | ReadOnly {readOnly}";
			}

			return result;
		}
	}
}
