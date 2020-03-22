using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ivory.Soap.Handlers
{
    /// <summary>SOAP fault handler.</summary>
    public static class SoapFaultHandler
    {
        /// <summary>Handles a SOAP Fault.</summary>
        public static Task Handle(HttpContext context)
        {
            Guard.NotNull(context, nameof(context));

            var ex = context.Features.Get<IExceptionHandlerFeature>();

            if (ex.Error is null)
            {
                return Task.CompletedTask;
            }

            var fault = new SoapFault(SoapFaultCode.Server, ex.Error.Message)
            {
                FaultActor = "Not me",
            };

            var message = SoapEnvelope.Fault(fault);

            var buffer = new MemoryStream();
            var settings = new SoapWriterSettings();
            message.Save(buffer, settings);
            buffer.Position = 0;

            return context.Response.WriteAsync(Encoding.UTF8.GetString(buffer.ToArray()), default);
        }
    }
}
