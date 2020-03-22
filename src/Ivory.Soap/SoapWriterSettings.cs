using Ivory.Soap.Xml;

namespace Ivory.Soap
{
    /// <summary>Specifies a set of features to support writing SOAP messages.</summary>
    public class SoapWriterSettings
    {
        /// <summary>Gets the collection of XML qualified names.</summary>
        public XmlQualifiedNameCollection QualifiedNames { get; } = new XmlQualifiedNameCollection()
            .Add("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            .Add(string.Empty, null);

        /// <summary>Gets or sets the preferred SOAP version.</summary>
        public SoapVersion SoapVersion { get; set; } = SoapVersion.V1_1;

        /// <summary>Gets and sets the SOAP encoding style.</summary>
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";
    }
}
