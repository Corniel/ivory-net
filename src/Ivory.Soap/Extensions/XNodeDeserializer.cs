using System.Xml.Serialization;

namespace System.Xml.Linq
{
    /// <summary>Extensions on <see cref="XNode"/> for deserialization.</summary>
    public static class XNodeDeserializer
    {
        /// <summary>Deserializes the XML node.</summary>
        /// <param name="node">
        /// The node to deserialize.
        /// </param>
        /// <param name="type">
        /// The type to deserialize it too.
        /// </param>
        /// <returns>
        /// A deserialized object.
        /// </returns>
        public static object Deserialize(this XNode node, Type type)
        {
            if (node is null)
            {
                return null;
            }

            if (node.GetType() == type)
            {
                return node;
            }

            var reader = node.CreateReader();
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(reader);
        }
    }
}
