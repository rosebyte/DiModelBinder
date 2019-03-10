using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.IntegrationTests
{
	public class InputWithBody
	{
		private readonly IMyService _service;

		public InputWithBody(IMyService service) => _service = service;

		[FromRoute]
		public int? Id { get; set; }

		[FromQuery]
		public DateTime Created { get; set; } = DateTime.MinValue;

		[FromBody]
		public ReadOnlyBody Body { get; set; }

		public async Task<IActionResult> Process()
		{
			var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created, Body.ReadOnly));
			return await Task.FromResult(result);
		}
	}
}
