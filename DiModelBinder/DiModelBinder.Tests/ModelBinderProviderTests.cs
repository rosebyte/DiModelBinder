using System;
using DiModelBinder.Tests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Moq;
using RoseByte.DiModelBinder;
using Xunit;
using MsILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace DiModelBinder.Tests
{
	public class ModelBinderProviderTests
	{
		[Fact]
		public void ShouldReturnBinder()
		{
			var bindingSource = new BindingSource(nameof(WithDiAttribute), nameof(WithDiAttribute), true, true);

			var metadata = new Mock<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(TestModel)));
			metadata.Setup(x => x.BindingSource).Returns(bindingSource);
			metadata.Setup(x => x.Properties).Returns(new ModelPropertyCollection(new ModelMetadata[] { }));

			var loggerFactory = new Mock<MsILoggerFactory>();

			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(typeof(MsILoggerFactory))).Returns(loggerFactory.Object);
			services.Setup(x => x.GetService(typeof(IDiResolver))).Returns(new DiResolver());

			var context = new Mock<ModelBinderProviderContext>();
			context.Setup(x => x.Metadata).Returns(metadata.Object);
			context.Setup(x => x.Services).Returns(services.Object);

			var sut = new ModelBinderProvider();

			var result = sut.GetBinder(context.Object);

			Assert.IsType<DiBinder>(result);
		}

		[Fact]
		public void ShouldReturnNull()
		{
			var bindingSource = BindingSource.Body;

			var metadata = new Mock<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(TestModel)));
			metadata.Setup(x => x.BindingSource).Returns(bindingSource);
			metadata.Setup(x => x.Properties).Returns(new ModelPropertyCollection(new ModelMetadata[] { }));

			var loggerFactory = new Mock<MsILoggerFactory>();

			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(typeof(MsILoggerFactory))).Returns(loggerFactory.Object);
			services.Setup(x => x.GetService(typeof(IDiResolver))).Returns(new DiResolver());

			var context = new Mock<ModelBinderProviderContext>();
			context.Setup(x => x.Metadata).Returns(metadata.Object);
			context.Setup(x => x.Services).Returns(services.Object);

			var sut = new ModelBinderProvider();

			var result = sut.GetBinder(context.Object);

			Assert.Null(result);
		}
	}
}
