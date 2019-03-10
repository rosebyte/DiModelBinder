using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.IntegrationTests
{
	public class Input
	{
		private readonly IMyService _service;

		public Input(IMyService service) => _service = service;

		[FromRoute]
		public int? Id { get; set; }

		[FromQuery]
		public DateTime Created { get; set; } = DateTime.MinValue;

		public async Task<IActionResult> Process()
		{
			var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created));
			return await Task.FromResult(result);
		}
	}
}
