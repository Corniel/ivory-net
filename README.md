# Ivory.NET SOAP
Ivory.NET SOAP is lightweight library on top of
[ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core).

## SOAP endpoint
A custom controller could look like:
``` C#
[SoapController(version: SoapVersion.V1_2, route: "/")]
public class CustomController : ControllerBase
{
    [SoapAction("http://custom.org/custom-action")]
    public IActionResult CustomAction(Header header, Body body)
    {
        // Do/call logic.
        return this.Soap(body: SomeResult());
    }
}
```
The `SoupAction` attribute is required, as without it, the routing could not
know, how to reach the specific endpoint. The `this.Soap()` extension method
ensures the correct output taking the `SoapVersion` attribute (1.2 in this
example), into account.

The Startup.cs should look a bit like this:

``` C#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ModelBinderProviders.Insert(0, new SoapModelBinder());
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
```
In other words, the only thing that really has to be done (besides enabling
ASP.NET Core itself) is the registration of the `SoapModelBinder`.
