using Ivory.Soap.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP fault.</summary>
    [Serializable]
    [XmlRoot("Fault", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapFault
    {
        /// <summary>Initializes a new instance of the <see cref="SoapFault"/> class.</summary>
        /// <param name="faultcode">
        /// The SOAP fault code.
        /// </param>
        /// <param name="faultString">
        /// The SOAP fault string.
        /// </param>
        public SoapFault(SoapFaultCode faultcode, string faultString)
        {
            FaultCode = Guard.DefinedEnum(faultcode, nameof(faultcode));
            FaultString = faultString;
        }

        /// <summary>Initializes a new instance of the <see cref="SoapFault"/> class.</summary>
        public SoapFault() { }

        /// <summary>Gets or sets the SOAP fault code.</summary>
        [XmlElement("faultcode", Namespace = "", Order = 0)]
        public SoapFaultCode FaultCode { get; set; }

        /// <summary>Gets or sets the SOAP fault string.</summary>
        [XmlElement("faultstring", Namespace = "", Order = 1)]
        public string FaultString { get; set; }

        /// <summary>Gets or sets the SOAP fault string.</summary>
        [XmlElement("faultactor", Namespace = "", Order = 2)]
        public string FaultActor { get; set; }

        /// <summary>Creates a SOAP fault for the model state.</summary>
        /// <remarks>
        /// The SOAP fault code is Client.
        /// </remarks>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        public static SoapFault FromModelState(ModelStateDictionary modelState)
        {
            Guard.NotNull(modelState, nameof(modelState));
            var error = modelState.GetErrors().FirstOrDefault();
            return new SoapFault(SoapFaultCode.Client, error?.Message);
        }
    }
}
