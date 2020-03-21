using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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

            var fault = new SoapFault1_2
            {
                Code = SoapFaultCode.Server,
                Subcode = ex.Error.GetType().Name,
                Reason = ex.Error.Message,
            };

            var message = new SoapMessage(header: null, body: fault);

            var buffer = new StringBuilder();
            var settings = SoapWriterSettings.V1_1;
            var writer = XmlWriter.Create(buffer, settings);

            message.Save(writer, settings);

            return context.Response.WriteAsync(buffer.ToString(), default);
        }
    }
}
