# DiModelBinder
Custom model binder for ASP Core resolving model binding with DI dependencies.

## Configuration
There are two things that needs to be configured in Startup.ConfigureServices method. First is to call extension method AddModelBindingDiResolver() on IServiceCollection parameter of the method. Second is to call extension method InsertDiModelBinderProvider() on MvcOptions parameter in UseMvc(options => ...) fragment.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.RegisterDiClients();
	services.AddMvc(o => o.InsertDiModelBinderProvider());
}
```

Default, the RegisterDiClients extension scan entry assembly and calling assembly, if you need to register some other assembly, this default can be overriden by filling these assemblies (note that it is an override so you must then explicitly use entry assembly and / or calling assembly if you want to scan them)

```csharp
services.RegisterDiClients(Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly());
```

## DiClient Attribute
After configuration we can start using new parameter attribute [DiClient]. This attribute marks classes that should be binded after dependency resolving. 

```csharp
[DiClient]
public class ExampleModel
{
	private readonly IMyService _service;

	public InputWithBody(IMyService service) => _service = service;

	[FromRoute]
	public int? Id { get; set; }

	public async Task<IActionResult> Process()
	{
		var result = new JsonResult(_service.FormatInputs(Id ?? 0));
		return await Task.FromResult(result);
	}
}
```

As default, lifespan of that class is transient. If you need some other lifespan, you can fill it in attribute's constructor

```csharp
[DiClient(ServiceLifetime.Scoped)]
public class ScopedExampleModel
{
	private level = 0;

	private readonly IMyService _service;

	public InputWithBody(IMyService service) => _service = service;

	[FromRoute]
	public int? Id { get; set; }

	public async Task<IActionResult> Process()
	{
		level++;
		var result = new JsonResult(_service.FormatInputs(level, Id));
		return await Task.FromResult(result);
	}
}
```

This attribute also marks that we want to do model binding with some model placed in ServiceContainer (if you use some class the way, you must place it to container manually).

```csharp
public Task<IActionResult> Get([DiClient]Input input) => input.Process();
```

## Binded class
The class can have five types of properties binded from query, route, header, body and of course classical no-binded property.

```csharp
[DiClient]
public class InputWithBody
{
	private readonly IMyService _service;

	public InputWithBody(IMyService service) => _service = service;

	[FromRoute]
	public int? Id { get; set; }

	[FromQuery]
	public DateTime Created { get; set; } = DateTime.MinValue;

	[FromHeader]
	public string UserName { get; set; }

	[FromBody]
	public ReadOnlyBody Body { get; set; }

	public bool Repeatable { get; set; } = false;

	public async Task<IActionResult> Process()
	{
		var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created, Body.ReadOnly));
		return await Task.FromResult(result);
	}
}
```

Please note that property with [FromBody] attribute is complex type. This is preferred way of binding complex type.
