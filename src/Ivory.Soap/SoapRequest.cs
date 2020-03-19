using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    internal static class SoapRequest
    {
        private static XNamespace NS = XNamespace.Get(SoapMessage.NS);

        public static bool IsSoapRequest(this HttpContext httpContext)
        {
            return httpContext.TryGetSoapAction(out _) == 1;
        }

        public static int TryGetSoapAction(this HttpContext httpContext, out string soapAction)
        {
            soapAction = default;
            if (httpContext.Request.Headers.TryGetValue(SoapClient.SOAPAction, out var values) && values.Count > 0)
            {
                soapAction = values[0];
                return values.Count;
            }
            return 0;
        }

        public static async Task<XElement> GetSoapRequestAsync(this HttpContext httpContext)
        {
            Guard.NotNull(httpContext, nameof(httpContext));

            try
            {
                var body = httpContext.Request.Body;
                if (!body.CanSeek)
                {
                    var buffer = new MemoryStream();
                    await body.CopyToAsync(buffer);
                    httpContext.Request.Body = buffer;
                    buffer.Position = 0;
                }

                return await XElement.LoadAsync(httpContext.Request.Body, LoadOptions.PreserveWhitespace, default);
            }
            catch
            {
                return null;
            }
            finally
            {
                httpContext.Request.Body.Position = 0;
            }

        }

        public static XElement GetSoapHeader(this XElement soapRequest)
        {
            return Guard.NotNull(soapRequest, nameof(SaveOptions))
                .Element(NS + SoapMessage.Header)?.Elements().FirstOrDefault();
        }

        public static XElement GetSoapBody(this XElement soapRequest)
        {
            return Guard.NotNull(soapRequest, nameof(SaveOptions))
                .Element(NS + SoapMessage.Body)?.Elements().FirstOrDefault();
        }

        public static object Deserialize(this XNode node, Type type)
        {
            if (node is null)
            {
                return null;
            }
            if (node.GetType() == type)
            {
                return node;
            }
            var reader = node.CreateReader();

            if (typeof(IXmlSerializable).IsAssignableFrom(type))
            {
                var xmlSerializable = (IXmlSerializable)Activator.CreateInstance(type);
                xmlSerializable.ReadXml(reader);
                return xmlSerializable;
            }
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(reader);
        }
    }
}
