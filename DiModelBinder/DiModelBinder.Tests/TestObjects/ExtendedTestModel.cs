namespace DiModelBinder.Tests.TestObjects
{
	public class ExtendedTestModel
	{
		private TestModel _model;
		private AnotherModel _another;

		public ExtendedTestModel(TestModel model, AnotherModel another)
		{
			_model = model;
			_another = another;
		}
	}
}