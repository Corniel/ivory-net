# Ivory.NET SOAP
Ivory.NET SOAP is lightweight library on top of
[ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core).

## SOAP endpoint
A custom controller could look like:
``` C#
[SoapController(route: "/")]
public class CustomController : ControllerBase
{
    [SoapAction("http://custom.org/custom-action")]
    public IActionResult CustomAction([SoapHeader]HeaderType header, [SoapBody]CustomType request)
    {
        // Do/call logic.
        return this.Soap(SomeResult());
    }
}
```
The `SoupAction` and `SoapController` attributes are required, as without
it, the routing could not know, how to reach the specific endpoint. The 
`this.Soap()` extension method ensures the correct output taking the 
`SoapController` preferences into account.

The Startup.cs should look a bit like this:

``` C#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ModelBinderProviders.Insert(0, new SoapModelBinderProvider());
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

## Model binding
When the `SoapModelBinderProvider` has been registered, the model binding works
when the parameter arguments are decorated with `FromSoapEnvelope`,
`FromSoapHeader`, or `FromSoapBody`. The types of the parameters should be
decorated with  the `Serializable` attribute.

An alternative is to use `XDocument` (only supported for the envelope) and
`XContainer` (to support multiple header or body nodes), and `XElement`
to retrieve exactly one header or body node.
