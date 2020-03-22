namespace Ivory.Soap
{
    /// <summary>Specifies a set of features to support writing SOAP messages.</summary>
    public class SoapWriterSettings
    {
        /// <summary>Gets or sets the namespace prefix for the SOAP namespace.</summary>
        public string NamespacePrefix { get; set; } = "soap";

        /// <summary>Gets or sets the preferred SOAP version.</summary>
        public SoapVersion SoapVersion { get; set; } = SoapVersion.V1_1;

        /// <summary>Gets or sets the preferred SOAP version.</summary>
        public string Namespace { get; set; } = "http://schemas.xmlsoap.org/soap/envelope/";

        /// <summary>Gets and sets the SOAP encoding style.</summary>
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";
    }
}
