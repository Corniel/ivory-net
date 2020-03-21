using System.Collections.Generic;
using System.Xml.Serialization;

namespace XXX
{
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapBody<TBody> : List<TBody>
    {
    }
}
