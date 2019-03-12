using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.DiModelBinder.Attributes;

namespace DiModelBinder.IntegrationTests
{
	public interface IInput
	{
		DateTime? Created { get; set; }
		int? Id { get; set; }
		Task<IActionResult> Process();
	}
}