using System;
using System.Collections.Generic;
using System.Text;

namespace DiModelBinder.Tests.TestObjects
{
	public class TestNonByCls
	{
		private object _model;

		public TestNonByCls(INonExisting model)
		{
			_model = model;
		}
	}
}
