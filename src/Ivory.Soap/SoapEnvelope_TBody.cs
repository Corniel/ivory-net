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
    {
        /// <summary>Gets and sets the SOAP body.</summary>
        [XmlElement(Order = 1)]
        public SoapBody<TBody> Body { get; set; } = new SoapBody<TBody>();
    }
}
