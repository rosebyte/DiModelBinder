using System;
using DiModelBinder.Tests.TestObjects;
using Moq;
using RoseByte.DiModelBinder;
using Xunit;

namespace DiModelBinder.Tests
{
	public class DiResolverTests
	{
		[Fact]
		public void ShouldReturnTypeFromDiIfItIsThere()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(typeof(ITestModel))).Returns(new TestModel());

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(ITestModel), services.Object);

			Assert.IsType<TestModel>(result);
		}

		[Fact]
		public void ShouldResolveTypeWithDefaultConstructor()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(TestModel), services.Object);

			Assert.IsType<TestModel>(result);
		}

		[Fact]
		public void ShouldResolveTypeWithNonEmptyConstructor()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(ExtendedTestModel), services.Object);

			Assert.IsType<ExtendedTestModel>(result);
		}

		[Fact]
		public void ShouldResolveTypeWithDefaultConstructorByInterface()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(ITestModel), services.Object);

			Assert.IsType<TestModel>(result);
		}

		[Fact]
		public void ShouldResolveInterfaceByParameterAttribute()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(TestNonByParam), services.Object);

			Assert.IsType<TestNonByParam>(result);
		}

		[Fact]
		public void ShouldResolveInterfaceByClassAttribute()
		{
			var services = new Mock<IServiceProvider>();
			services.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

			var sut = new DiResolver();

			var result = sut.ResolveModel(typeof(TestNonByCls), services.Object);

			Assert.IsType<TestNonByCls>(result);
		}
	}
}
