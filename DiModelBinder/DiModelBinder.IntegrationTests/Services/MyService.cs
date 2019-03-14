using System;

namespace DiModelBinder.IntegrationTests
{
	public class MyService : IMyService
	{
		public string FormatInputs(int id, DateTime created, string header, bool? readOnly = null)
		{
			var result = $"ID: {id} | Created: {created:MM/dd/yyyy}";

			if (!string.IsNullOrWhiteSpace(header))
			{
				result += $" | UserName: {header}";
			}

			if (readOnly.HasValue)
			{
				result += $" | ReadOnly {readOnly}";
			}

			return result;
		}
	}
}
