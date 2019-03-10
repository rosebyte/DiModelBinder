using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace DiModelBinder.Tests
{
	public class Configuration
	{
		[Fact]
		public void ShouldAddModelBinderProvider()
		{
			var options = new MvcOptions();
			options.InsertDiModelBinderProvider();

			Assert.Equal(1 , options.ModelBinderProviders.Count);
		}

		[Fact]
		public void ShouldAddDiResolver()
		{
			var services = new Mock<IServiceCollection>();
			services.Object.AddModelBindingDiResolver();

			services.Verify(x => x.Add(It.IsAny<ServiceDescriptor>()));
		}
	}
}
