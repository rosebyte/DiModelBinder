using System;

namespace DiModelBinder.IntegrationTests
{
	public interface IMyService
	{
		string FormatInputs(int id, DateTime created, string header, bool? readOnly = null);
	}
}
