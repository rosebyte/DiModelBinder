using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.IntegrationTests
{
	public interface IInput
	{
		DateTime Created { get; set; }
		int? Id { get; set; }
		Task<IActionResult> Process();
	}
}