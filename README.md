# DiModelBinder
Custom model binder for ASP Core resolving model binding with DI dependencies.

## Configuration
There are two things that needs to be configured in Startup.ConfigureServices method. First is to call extension method AddModelBindingDiResolver() on IServiceCollection parameter of the method. Second is to call extension method InsertDiModelBinderProvider() on MvcOptions parameter in UseMvc(options =>... fragment.

```csharp
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddModelBindingDiResolver();
    services.AddMvc(o => o.InsertDiModelBinderProvider());
  }
```

## WithDi Attribute
After configuration we can start using new parameter attribute [WithDi]. This attribute marks that we want to do model binding to some class with constructor dependencies.

```csharp
  public Task<IActionResult> Get([WithDi]Input input) => input.Process();
```

## Binded class
The class can have three types of properties binded from query, route or body.

```csharp
  public class InputWithBody
	{
		private readonly IMyService _service;

		public InputWithBody(IMyService service) => _service = service;

		[FromRoute]
		public int? Id { get; set; }

		[FromQuery]
		public DateTime Created { get; set; } = DateTime.MinValue;

		[FromBody]
		public ReadOnlyBody Body { get; set; }

		public async Task<IActionResult> Process()
		{
			var result = new JsonResult(_service.FormatInputs(Id ?? 0, Created, Body.ReadOnly));
			return await Task.FromResult(result);
		}
	}
```
Please note that property with [FromBody] attribute is complex type. This is preferred way of binding complex type.

## Dynamic resolving
Dependencies are primarily resolved via framework's DI (to use it we must add these dependencies to DI container at startup). However, sometimes this can be too restrictive and boilerplate prone. That's why some dynamic resolving was added. This resolving can resolve class dependencies and interface dependencies with just one implementation containing resolvable constructor (resolvable constructor is empty or containing only resolvable dependencies).

```csharp
  public InputWithBody(IMyService service, User user)
```
This constructor is OK when User has resolvable constructor and IMyservice is implemented by one class with at least one resolvable constructor. Of course, if any of these dependencies are in framework's DI container, it's allways OK.

## ResolveWith attribute
There is one way to help MVC with resolving - [ResolveWith] attribute. It can be placed on parameter or interface and accepts name of class definition to resolve given dependency. When on parameter it says that the parameter should be resolved with particular type, 

```csharp
  public InputWithBody([ResolveWith(typeof(SomeService))]IMyService service)
```

when on interface, it says that this interface should be resolved with the type.

```csharp
  [ResolveWith(typeof(OrderRepository))]
	public interface IRepository
	{ }
```
