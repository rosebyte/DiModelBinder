using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.DiModelBinder.Attributes;

namespace DiModelBinder.IntegrationTests
{
	public class UnregisteredInput
	{
		private readonly IMyService _service;

		public UnregisteredInput(IMyService service) => _service = service;

		[FromRoute]
		public int? Id { get; set; }

		[FromHeader]
		public string UserName { get; set; }

		[FromQuery]
		public DateTime? Created { get; set; }



		public async Task<IActionResult> Process()
		{
			var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created ?? DateTime.MinValue, UserName));
			return await Task.FromResult(result);
		}
	}
}
