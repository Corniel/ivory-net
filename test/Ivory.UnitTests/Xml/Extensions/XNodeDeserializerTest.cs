using Ivory.TestModels;
using NUnit.Framework;
using System.Xml.Linq;

namespace Ivory.UnitTests.Xml.Extensions
{
    public class XNodeDeserializerTest
    {
        [Test]
        public void Deserialize_Null_IsNull()
        {
            XElement xText = null;

            var deserialized =  xText.Deserialize(typeof(SimpleBody));
            Assert.IsNull(deserialized);
        }
        [Test]
        public void Deserialize_ToSameType_AreIdentical()
        {
            var expected = new XElement("dummy", "2624DP");
            var deserialized = expected.Deserialize(typeof(XElement));

            Assert.AreSame(expected, deserialized);
        }
    }
}
