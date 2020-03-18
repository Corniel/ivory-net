using System.Threading.Tasks;
using System.Xml;

namespace Ivory.Soap
{
    /// <summary>Helper method for SOAP message literals.</summary>
    public static class SoapMessage
    {
        /// <summary>Gets the SOAP message namespace.</summary>
        public static readonly string NS = "http://www.w3.org/2001/12/soap-envelope";

        /// <summary>Gets the SOAP message encoding style.</summary>
        public static readonly string EncodingStyle = "http://www.w3.org/2001/12/soap-encoding";

        /// <summary>Gets the SOAP message namespace prefix.</summary>
        public static readonly string Prefix = "SOAP-ENV";

        /// <summary>Gets the SOAP envelope.</summary>
        public static readonly string Envelope = nameof(Envelope);

        /// <summary>Gets the SOAP header.</summary>
        public static readonly string Header = nameof(Header);

        /// <summary>Gets the SOAP body.</summary>
        public static readonly string Body = nameof(Body);

        /// <summary>Gets the SOAP fault.</summary>
        public static readonly string Fault = nameof(Fault);

        internal static Task WriteSoapElementAsync(this XmlWriter xmlWriter, string localName)
        {
            return xmlWriter.WriteStartElementAsync(Prefix, localName, NS);
        }
    }
}
