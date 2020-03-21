using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XXX
{
    [Serializable]
    [XmlRoot("Fault")]
    public class SoapFault
    {
        /// <summary>Gets the SOAP fault code.</summary>
        [XmlElement("faultcode", Namespace = "")]
        public SoapFaultCode FaultCode { get; set; }

        /// <summary>Gets the SOAP fault string.</summary>
        [XmlElement("faultstring", Namespace = "")]
        public string FaultString { get; set; }
    }
}
