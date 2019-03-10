﻿using System;
using System.Collections.Generic;

namespace DiModelBinder
{
	public interface IDiResolver
	{
		object ResolveModel(Type type, IServiceProvider provider, IEnumerable<Attribute> attributes = null);
	}
}