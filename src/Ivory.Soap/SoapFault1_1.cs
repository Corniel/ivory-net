using Ivory.Soap.Extensions;
using Ivory.Soap.Modelbinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;
using System.Linq;
using System.Xml;

namespace Ivory.Soap
{
    public class SoapFault1_1 : ISoapWritable
    {
        public SoapFault1_1(SoapFaultCode faultCode, string faultString, object detail)
        {
            FaultCode = Guard.DefinedEnum(faultCode, nameof(faultCode));
            FaultString = faultString;
            Detail = detail;
        }

        /// <summary>Gets the SOAP fault code.</summary>
        public SoapFaultCode FaultCode { get; }

        /// <summary>Gets the SOAP fault string.</summary>
        public string FaultString { get; }

        /// <summary>Gets the SOAP fault details.</summary>
        public object Detail { get; }

        /// <inheritdoc/>
        public void Save(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));
            xmlWriter
                .WriteSoapElement("Fault", settings)
                .WriteElementIfNotNull("faultcode", FaultCode)
                .WriteElementIfNotNull("faultstring", FaultString)
            ;
            if (Detail != null)
            {
                xmlWriter.WriteStartElement("detail");

                if (Detail.GetType().IsArray)
                {
                    foreach (var child in (IEnumerable)Detail)
                    {
                        xmlWriter.WriteContent(child, settings);
                    }
                }
                else
                {
                    xmlWriter.WriteContent(Detail, settings);
                }
                xmlWriter.WriteCloseElement();
            }
            xmlWriter
                .WriteCloseElement();
        }

        /// <summary>Creates a SOAP fault v1.1 based on the model state.</summary>
        public static SoapFault1_1 FromModelState(ModelStateDictionary modelState)
        {
            return new SoapFault1_1(
                SoapFaultCode.Client,
                "Invalid request",
                modelState.GetErrors().ToArray()
            );
        }
    }
}
