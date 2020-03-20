using NUnit.Framework;
using Qowaiv;
using System.Xml.Linq;

namespace Ivory.Soap.UnitTests.Extensions
{
    public class XNodeDeserializeTest
    {
        [Test]
        public void Deserialize_Null_IsNull()
        {
            XElement xText = null;

            var deserialized = xText.Deserialize(typeof(PostalCode));
            Assert.IsNull(deserialized);
        }
        [Test]
        public void Deserialize_ToSameType_AreIdentical()
        {
            var expected = new XElement("dummy", "2624DP");
            var deserialized = expected.Deserialize(typeof(XElement));

            Assert.AreSame(expected, deserialized);
        }

        [Test]
        public void Deserialize_IXmlSerializable_IsDeserialzed()
        {
            var xText = new XElement("dummy", "2624DP");

            var deserialized = xText.Deserialize(typeof(PostalCode));
            var expected = PostalCode.Parse("2624DP");

            Assert.AreEqual(expected, deserialized);
        }
    }
}
