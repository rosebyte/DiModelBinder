using System;
using System.Threading.Tasks;
using DiModelBinder.Integration.InputModels;
using Microsoft.AspNetCore.Mvc;
using RoseByte.DiModelBinder;

namespace DiModelBinder.IntegrationTests.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PerformanceController : ControllerBase
	{
		[HttpGet("dimodelbinder")]
		public Task<IActionResult> DiModelBinder(PerfInput input)
			=> input.Process();

		[HttpGet("requestinjector")]
		public Task<IActionResult> RequestInjector(PerfInput input)
			=> input.Process();
	}
}