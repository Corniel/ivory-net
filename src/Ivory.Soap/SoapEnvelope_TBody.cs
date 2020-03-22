using System;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP envelope.</summary>
    /// <typeparam name="TBody">
    /// The type of the body content.
    /// </typeparam>
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope<TBody> : SoapEnvelope
        where TBody : class
    {
        /// <summary>Gets and sets the SOAP body.</summary>
        public SoapContent<TBody> Body { get; set; }
    }
}
