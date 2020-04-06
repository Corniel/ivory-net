using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP content placeholder.</summary>
    /// <typeparam name="TContent">
    /// The type of the content.
    /// </typeparam>
    /// <remarks>
    /// SOAP content implements <see cref="IXmlSerializable"/> so that it can
    /// prevent its child(ren) from having the SOAP namespace.
    /// </remarks>
    [Serializable]
    public class SoapContent<TContent> : List<TContent>, IXmlSerializable
        where TContent : class
    {
        /// <summary>Initializes a new instance of the <see cref="SoapContent{TContent}"/> class.</summary>
        public SoapContent() { }

        /// <summary>Initializes a new instance of the <see cref="SoapContent{TContent}"/> class.</summary>
        /// <param name="content">
        /// The item to add.
        /// </param>
        public SoapContent(TContent content) : base(1)
        {
            if (content is TContent)
            {
                Add(content);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="SoapContent{TContent}"/> class.</summary>
        /// <param name="content">
        /// The items to add.
        /// </param>
        public SoapContent(IEnumerable<TContent> content)
        {
            Guard.NotNull(content, nameof(content));
            AddRange(content);
        }

        /// <inheritdoc/>
        public XmlSchema GetSchema() => null;

        /// <inheritdoc/>
        public void ReadXml(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));
            var serializer = new XmlSerializer(typeof(TContent), string.Empty);

            while (reader.Read())
            {
                var item = (TContent)serializer.Deserialize(reader);
                if (item != null)
                {
                    Add(item);
                }
            }
        }

        /// <inheritdoc/>
        public void WriteXml(XmlWriter writer)
        {
            Guard.NotNull(writer, nameof(writer));
            var serializer = new XmlSerializer(typeof(TContent));

            foreach (var item in this)
            {
                serializer.Serialize(writer, item, SoapXml.Empty);
            }
        }
    }
}
