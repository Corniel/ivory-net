using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Ivory.Soap.Extensions;

namespace Ivory.Soap
{

    public class SoapFault1_2 : ISoapWritable
    {
        private const string Fault = nameof(Fault);

        public SoapFaultCode? Code { get; set; }
        public string Subcode { get; set; }
        public string Reason { get; set; }
        public string Actor { get; set; }
        public object Detail { get; set; }

        /// <inheritdoc/>
        public void Save(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));
            settings ??= SoapWriterSettings.V1_2;

            //if (settings?.SoapVersion.Version == SoapVersion.1_1.Version)
            //{
            //    SaveV1_1(xmlWriter, settings);
            //}
            //else
            //{
            //    SaveV1_2(xmlWriter, settings);
            //}
        }

        private void SaveV1_2(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            xmlWriter
                .WriteSoapElement(Fault, settings);
            {
                xmlWriter
                .WriteSoapElement(nameof(Code), settings)
                .WriteElementString(settings.NamespacePrefix, "Value", settings.Namespace, Code.ToString());

                xmlWriter
                    .WriteCloseElement()
                ;
            }
            xmlWriter.WriteCloseElement();
        }

        private void SaveV1_1(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            xmlWriter
                .WriteSoapElement(Fault, settings)
                .WriteElementIfNotNull("faultcode", Code)
                .WriteElementIfNotNull("faultstring", Reason)
                .WriteElementIfNotNull("faultactor", Actor)
                .WriteCloseElement();
        }
    }
}
