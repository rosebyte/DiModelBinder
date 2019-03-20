using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiModelBinder.IntegrationTests;
using Microsoft.AspNetCore.Mvc;
using RoseByte.DiModelBinder;

namespace DiModelBinder.Integration.InputModels
{
	[DiClient]
	public class PerfInput
	{
		private readonly IMyService _service;
		public PerfInput(IMyService service) => _service = service;

		public async Task<IActionResult> Process()
		{
			var result = new JsonResult("Process performed.");
			return await Task.FromResult(result);
		}
	}
}
