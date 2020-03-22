using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ivory.Soap.Handlers
{
    public static class SoapFaultHandler
    {
        public static Task Handle(HttpContext context)
        {
            Guard.NotNull(context, nameof(context));

            var ex = context.Features.Get<IExceptionHandlerFeature>();

            if (ex.Error is null)
            {
                return Task.CompletedTask;
            }

            var fault = new SoapFault
            {
                FaultCode = SoapFaultCode.Server,
                FaultString = ex.Error.Message,
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
