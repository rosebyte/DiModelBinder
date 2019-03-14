using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoseByte.DiModelBinder.Attributes;

namespace DiModelBinder.IntegrationTests.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpGet("unregistered/{id}")]
		public Task<IActionResult> Get([DiClient] UnregisteredInput input)
			=> input.Process();

		[HttpGet("{id}")]
		public Task<IActionResult> Get(Input input, DateTime date)
			=> input.Process();

		[HttpPost("{id}")]
		public Task<IActionResult> Post(InputWithBody withBody, DateTime date) 
			=> withBody.Process();
	}
}