using Microsoft.AspNetCore.Http;
using System;
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
            try
            {
                return await XElement.LoadAsync(httpContext.Request.Body, LoadOptions.PreserveWhitespace, default);
            }
            catch
            {
                return null;
            }

        }

        public static object GetSoapHeader(this XElement soapRequest, Type headerType)
        {
            return Guard.NotNull(soapRequest, nameof(SaveOptions))
                .Element(NS + SoapMessage.Header)
                .Deserialize(headerType);
        }

        public static object GetSoapBody(this XElement soapRequest, Type headerType)
        {
            return Guard.NotNull(soapRequest, nameof(SaveOptions))
                .Element(NS + SoapMessage.Body)
                .Deserialize(headerType);
        }

        private static object Deserialize(this XNode node, Type type)
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
