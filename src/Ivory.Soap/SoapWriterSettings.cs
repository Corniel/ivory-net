using Ivory.Soap.Mvc;
using Ivory.Xml;

namespace Ivory.Soap
{
    /// <summary>Specifies a set of features to support writing SOAP messages.</summary>
    public class SoapWriterSettings
    {
        /// <summary>Gets the collection of XML qualified names.</summary>
        public XmlQualifiedNameCollection QualifiedNames { get; } = new XmlQualifiedNameCollection()
            .Add("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            .Add(string.Empty, null);

        /// <summary>Gets or sets the SOAP encoding style.</summary>
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";

        /// <summary>Gets the <see cref="SoapWriterSettings"/> from the controller if available.</summary>
        /// <param name="controller">
        /// The ASP.NET Core controller.
        /// </param>
        public static SoapWriterSettings FromController(object controller)
        {
            return controller is ISoapController soapController
                ? soapController.WriterSettings
                : null;
        }
    }
}
