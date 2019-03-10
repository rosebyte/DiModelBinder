using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiModelBinder.Tests.TestObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DiModelBinder.Tests
{
	public class DiBinderTests
	{
		[Fact]
		public async void ShouldAddModelBinderProvider()
		{
			var logger = new Mock<ILogger>();
			var loggerFactory = new Mock<ILoggerFactory>();
			loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
				.Returns(logger.Object);

			var resolver = new Mock<IDiResolver>();
			resolver.Setup(x => x.ResolveModel(typeof(TestModel), It.IsAny<IServiceProvider>(), null))
				.Returns(new TestModel());

			var bindingSource = new BindingSource(nameof(WithDiAttribute), nameof(WithDiAttribute), true, true);

			var prop = new Mock<ModelMetadata>(ModelMetadataIdentity.ForProperty(typeof(TestModel), "Name", typeof(string)));
			prop.Setup(x => x.IsBindingAllowed).Returns(true);
			prop.Setup(x => x.BindingSource).Returns(bindingSource);

			

			var metadata = new Mock<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(TestModel)));
			metadata.Setup(x => x.Properties).Returns(new ModelPropertyCollection(new [] { prop.Object  }));

			var services = new Mock<IServiceProvider>();

			var httpContext = new Mock<HttpContext>();
			httpContext.Setup(x => x.RequestServices).Returns(services.Object);

			var context = new Mock<ModelBindingContext>();
			context.Setup(x => x.ModelMetadata).Returns(metadata.Object);
			context.Setup(x => x.HttpContext).Returns(httpContext.Object);
			context.Setup(x => x.ModelType).Returns(typeof(TestModel));
			context.Setup(x => x.Result).Returns(ModelBindingResult.Success(new TestModel()));

			var binder = new Mock<IModelBinder>();
			binder.Setup(x => x.BindModelAsync(It.IsAny<ModelBindingContext>())).Returns(Task.CompletedTask)
				.Callback((ModelBindingContext y) => y.Result = ModelBindingResult.Success(new TestModel()));

			var sut = new DiBinder(
				new Dictionary<ModelMetadata, IModelBinder>{{metadata.Object, binder.Object}},
				loggerFactory.Object, 
				resolver.Object);

			try
			{
				await sut.BindModelAsync(context.Object);
			}
			catch
			{}

			resolver.Verify(x => x.ResolveModel(typeof(TestModel), It.IsAny<IServiceProvider>(), null));
		}
	}
}

