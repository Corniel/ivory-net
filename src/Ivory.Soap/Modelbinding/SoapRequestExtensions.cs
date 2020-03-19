using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ivory.Soap.Modelbinding
{
    internal static class SoapRequestExtensions
    {
        public static bool IsSoapRequest(this HttpContext httpContext)
        {
            return httpContext.TryGetSoapAction(out _) == 1;
        }

        public static int TryGetSoapAction(this HttpContext httpContext, out string soapAction)
        {
            soapAction = default;
            if (httpContext.Request.Headers.TryGetValue(SoapMessage.SOAPAction, out var values) && values.Count > 0)
            {
                soapAction = values[0];
                return values.Count;
            }
            return 0;
        }
    }
}
