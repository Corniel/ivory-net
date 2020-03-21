using Ivory.Soap.Extensions;
using Ivory.Soap.Modelbinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    [XmlRoot("Fault", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapFault1_1 : SoapFault
    {
        public SoapFault1_1() { }

        public SoapFault1_1(SoapFaultCode faultCode, string faultString)
        {
            FaultCode = Guard.DefinedEnum(faultCode, nameof(faultCode));
            FaultString = faultString;
        }

        /// <summary>Gets the SOAP fault code.</summary>
        [XmlElement("faultcode", Namespace = "")]
        public SoapFaultCode FaultCode { get; set; }

        /// <summary>Gets the SOAP fault string.</summary>
        [XmlElement("faultstring", Namespace = "")]
        public string FaultString { get; set; }

        /// <inheritdoc/>
        public override void Save(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));
            xmlWriter
                .WriteSoapElement("Fault", settings)
                .WriteElementIfNotNull("faultcode", FaultCode)
                .WriteElementIfNotNull("faultstring", FaultString)
            ;
            //if (Detail != null)
            //{
            //    xmlWriter.WriteStartElement("detail");

            //    if (Detail.GetType().IsArray)
            //    {
            //        foreach (var child in (IEnumerable)Detail)
            //        {
            //            xmlWriter.WriteContent(child, settings);
            //        }
            //    }
            //    else
            //    {
            //        xmlWriter.WriteContent(Detail, settings);
            //    }
            //    xmlWriter.WriteCloseElement();
            //}
            xmlWriter
                .WriteCloseElement();
        }

        /// <summary>Creates a SOAP fault v1.1 based on the model state.</summary>
        public static SoapFault1_1<BindingError> FromModelState(ModelStateDictionary modelState)
        {
            return new SoapFault1_1<BindingError>
            {
                FaultCode = SoapFaultCode.Client,
                FaultString = "Invalid request",
                Details = modelState.GetErrors().ToArray(),
            };
        }
    }

    public class SoapFault1_1<TDetail> : SoapFault1_1
    {
        /// <summary>Gets the SOAP fault details.</summary>
        [XmlElement("detail", Namespace = "")]
        public TDetail[] Details { get; set; }
    }
}
