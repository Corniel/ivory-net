using Ivory.Soap.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Ivory.Soap
{
    /// <summary>Represents a SOAP message.</summary>
    public class SoapMessage : ISoapWritable
    {
        /// <summary>Gets the SOAP envelope.</summary>
        private const string Envelope = nameof(Envelope);

        /// <summary>Initializes a new instance of the <see cref="SoapMessage"/> class.</summary>
        /// <param name="header">
        /// The (optional) header of the SOAP message.
        /// </param>
        /// <param name="body">
        /// The body of the SOAP message.
        /// </param>
        public SoapMessage(object header, object body)
        {
            Header = header;
            Body = Guard.NotNull(body, nameof(body));
        }

        /// <summary>Gets the SOAP header.</summary>
        public object Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public object Body { get; }

        /// <inheritdoc/>
        public void Save(XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));

            settings ??= SoapWriterSettings.V1_2;

            xmlWriter
                .WriteSoapElement(Envelope, settings)
                .WriteSoapHeader(Header, settings)
                .WriteSoapBody(Body, settings)
                .WriteCloseElement()
                .Flush()
            ;
        }

        public static Task<SoapMessage> LoadAsync(Stream stream) => LoadAsync(stream, typeof(XElement), typeof(XElement));

        public static async Task<SoapMessage> LoadAsync(Stream stream, Type headerType, Type bodyType)
        {
            Guard.NotNull(stream, nameof(stream));

            var root = await XElement.LoadAsync(stream, LoadOptions.None, default);

            var header = root.Element(root.Name.Namespace + nameof(Header))?
                .Elements()
                .FirstOrDefault()
                .Deserialize(headerType);

            var body = root.Element(root.Name.Namespace + nameof(Body))?
                .Elements()
                .FirstOrDefault()
                .Deserialize(bodyType);

            return new SoapMessage(header, body);
        }
    }
}
