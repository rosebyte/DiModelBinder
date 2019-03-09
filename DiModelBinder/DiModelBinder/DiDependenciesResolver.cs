using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace DiModelBinder
{
	public class DiDependenciesResolver
	{
		private static readonly Dictionary<Type, ObjectActivator> Creators = new Dictionary<Type, ObjectActivator>();

		public object ResolveModel(Type type, IServiceProvider provider, IEnumerable<Attribute> attributes = null)
		{
			var result = provider.GetService(type);

			if (result == null)
			{
				if (!Creators.TryGetValue(type, out var creator))
				{
					if (type.IsInterface || type.IsAbstract)
					{
						var other = FindType(type, provider, attributes);
						if (!Creators.TryGetValue(other, out var otherInst))
						{
							otherInst = CreateCreator(other, provider);
						}

						Creators[type] = creator = otherInst;
					}
					else
					{
						Creators[type] = creator = CreateCreator(type, provider);
					}

				}

				result = creator();
			}

			return result;
		}

		private Type FindType(Type type, IServiceProvider provider, IEnumerable<Attribute> attributes)
		{
			var dtype = attributes.FirstOrDefault(x => x.GetType() == typeof(ResolveWithAttribute));
			if (dtype != null)
			{
				return ((ResolveWithAttribute)dtype).Type;
			}

			var classTypeAttributes = type.GetCustomAttributes();
			var ctype = classTypeAttributes.FirstOrDefault(x => x.GetType() == typeof(ResolveWithAttribute));
			if (ctype != null)
			{
				return ((ResolveWithAttribute)ctype).Type;
			}

			var results = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(type2 => type.IsAssignableFrom(type))
				.FirstOrDefault(x => x.GetConstructors()
					.OrderBy(y => y.GetParameters().Length)
					.Any(y => y.GetParameters()
						.All(z => ResolveModel(z.ParameterType, provider) != null)));

			return results;
		}

		private ObjectActivator CreateCreator(Type type, IServiceProvider provider)
		{
			var constructors = type.GetConstructors()
				.OrderBy(x => x.GetParameters().Length);

			foreach (var constructor in constructors)
			{
				var resol = constructor.GetParameters()
					.Select(x => new { Type = x.ParameterType, Value = ResolveModel(x.ParameterType, provider) })
					.ToArray();

				if (resol.Any(x => x.Value == null))
				{
					continue;
				}

				var paramResol = resol
					.Select(x => Expression.Constant(x.Value, x.Type) as Expression)
					.ToArray();

				var constructorExp = Expression.New(constructor, paramResol);
				var paramExp = Expression.Parameter(typeof(object[]), "args");

				return Expression.Lambda<ObjectActivator>(constructorExp, paramExp).Compile();
			}

			throw new Exception("Model could not be resolved");
		}

		private delegate object ObjectActivator(params object[] args);
	}
}
