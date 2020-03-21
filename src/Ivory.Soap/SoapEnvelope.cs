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
    public class SoapEnvelope<TBody>
    {
        /// <summary>Gets and sets the SOAP encoding style.</summary>
        [XmlAttribute("encodingStyle")]
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";

        /// <summary>Gets and sets the SOAP body.</summary>
        [XmlElement(Order = 1)]
        public SoapBody<TBody> Body { get; set; }
    }

    /// <summary>Represents the SOAP envelope.</summary>
    /// <typeparam name="THeader">
    /// The type of the header content.
    /// </typeparam>
    /// <typeparam name="TBody">
    /// The type of the body content.
    /// </typeparam>
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope<THeader, TBody> : SoapEnvelope<TBody>
    {
        /// <summary>Gets and sets the SOAP body.</summary>
        [XmlElement(Order = 0)]
        public SoapHeader<THeader> Header { get; set; }
    }
}
