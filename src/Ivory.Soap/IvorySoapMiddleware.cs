using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ivory.Soap
{
    public class IvorySoapMiddleware
    {
        public IvorySoapMiddleware(RequestDelegate next)
        {
             Next = next;
        }

        public RequestDelegate Next { get; }

        /// <summary>Invokes the middleware for a request.</summary>
        public async Task Invoke(HttpContext context)
        {
            Guard.NotNull(context, nameof(context));

            var soapAction = context.Request.Headers[SoapClient.SOAPAction];

            // TODO: implement.
            throw new NotImplementedException($"Could not resolve SOAPAction: {soapAction}");

            await CallNextModuleIfPresent(context);
        }

        private Task CallNextModuleIfPresent(HttpContext context)
        {
            return Next != null ? Next.Invoke(context) : Task.CompletedTask;
        }
    }
}
