﻿using Microsoft.AspNetCore.Diagnostics;
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

            var message = new SoapEnvelope<SoapFault>();
            message.Body.Add(fault);

            var buffer = new MemoryStream();
            var settings = SoapWriterSettings.V1_1;
            message.Save(buffer, settings);
            buffer.Position = 0;

            return context.Response.WriteAsync(Encoding.UTF8.GetString(buffer.ToArray()), default);
        }
    }
}
