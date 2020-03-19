using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            switch (context.TryGetSoapAction(out var soapAction))
            {
                case 0:
                    await CallNextModuleIfPresent(context);
                    return;
                case 1:
                    break;
                default:
                    // bad request. multiple SOAPActions
                    break;
            }

            var soapRequest = await context.GetSoapRequestAsync();

            if (soapRequest is null)
            {
                // bad request. invalid SOAP
            }

            var header = soapRequest.GetSoapHeader(typeof(XElement));
            var body = soapRequest.GetSoapBody(typeof(XElement));

            await CallNextModuleIfPresent(context);
        }

        private Task CallNextModuleIfPresent(HttpContext context)
        {
            return Next != null ? Next.Invoke(context) : Task.CompletedTask;
        }
    }
}
