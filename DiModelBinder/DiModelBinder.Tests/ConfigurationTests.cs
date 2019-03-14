using Microsoft.AspNetCore.Mvc;
using RoseByte.DiModelBinder;
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
		public void ShouldRegisterType()
		{
			var options = new MvcOptions();
			options.InsertDiModelBinderProvider();

			Assert.Equal(1, options.ModelBinderProviders.Count);
		}

		[Fact]
		public void ShouldRegisterTypeForInterfaces()
		{
			var options = new MvcOptions();
			options.InsertDiModelBinderProvider();

			Assert.Equal(1, options.ModelBinderProviders.Count);
		}
	}
}
