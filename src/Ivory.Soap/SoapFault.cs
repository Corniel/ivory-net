using Ivory.Soap.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP fault.</summary>
    [Serializable]
    [XmlRoot("Fault")]
    public class SoapFault
    {
        /// <summary>Gets the SOAP fault code.</summary>
        [XmlElement("faultcode", Namespace = "", Order = 0)]
        public SoapFaultCode FaultCode { get; set; }

        /// <summary>Gets the SOAP fault string.</summary>
        [XmlElement("faultstring", Namespace = "", Order = 1)]
        public string FaultString { get; set; }

        /// <summary>Creates a SOAP fault for the model state.</summary>
        /// <remarks>
        /// The SOAP fault code is Client.
        /// </remarks>
        public static SoapFault FromModelState(ModelStateDictionary modelState)
        {
            var error = modelState.GetErrors().FirstOrDefault();
            return new SoapFault
            {
                FaultCode = SoapFaultCode.Client,
                FaultString = error?.Message,
            };
        }
    }
}
