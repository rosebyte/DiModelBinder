using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder.IntegrationTests
{
	public class TestInputModel2
	{
		private readonly IMyService _service;

		public TestInputModel2(IMyService service) => _service = service;

		[FromRoute]
		public int? Id { get; set; }

		[FromQuery]
		public DateTime Created { get; set; } = DateTime.MinValue;

		[FromBody]
		public bool ReadOnly { get; set; }

		public async Task<IActionResult> Process()
		{
			var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created, ReadOnly));
			return await Task.FromResult(result);
		}
	}
}
