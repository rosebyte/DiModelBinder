using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.IntegrationTests.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpGet("{id}")]
		public Task<IActionResult> Get([WithDi]Input input, DateTime date)
			=> input.Process();

		[HttpPost("{id}")]
		public Task<IActionResult> Post([WithDi]InputWithBody withBody, DateTime date) 
			=> withBody.Process();
	}
}