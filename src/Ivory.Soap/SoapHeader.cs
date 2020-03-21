using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP header placeholder.</summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapHeader<TBody> : List<TBody> { }
}
