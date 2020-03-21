using System.Net;
using System.Xml;

namespace Ivory.Soap
{
    /// <summary>Represents a SOAP fault.</summary>
    public abstract class SoapFault /*: ISoapWritable*/
    {
        /// <summary>Gets the HTTP status code that should be communicated with this fault.</summary>
        public virtual HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

        /// <inheritdoc/>
        public abstract void Save(XmlWriter xmlWriter, SoapWriterSettings settings);
    }
}
