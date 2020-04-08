using Ivory.UnitTests.TestTools;
using Ivory.Xml;
using NUnit.Framework;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ivory.UnitTests.Xml
{
    public class ExtendedXmlReaderTest
    {

        [Test]
        public void Extend_SkipComments_XmlWithoutComments()
        {
            using var reader = Message.EmbeddedReader("XmlReader.SkipComments.xml")
                .Extend(XmlReadExtensions.SkipComments);

            var xmlString = XDocument.Load(reader).ToString();

            Console.WriteLine(xmlString);

            var expected = Message.EmbeddedText("XmlReader.SkipComments.Expected.xml");

            Assert.AreEqual(expected, xmlString);
        }

        [Test]
        public void Deserialize_StrictOrder()
        {
            using var reader = Message.EmbeddedReader("StrictOrder.xml");

            var root = new XmlRootAttribute("StrictOrder2");

            var overrides = new XmlAttributeOverrides();
            
            foreach(var prop in typeof(StrictOrder).GetProperties())
            {
                var attributes = new XmlAttributes();
                var attr = new XmlElementAttribute(prop.Name, prop.PropertyType) {/* Order = -1, no order */ };
                attributes.XmlElements.Add(attr);
                overrides.Add(prop.DeclaringType, prop.Name, attributes);
            }
                    
            var serializer = new XmlSerializer(typeof(StrictOrder), overrides, Array.Empty<Type>(), root, "");
            serializer.UnknownNode += onUnknownNode;
            serializer.UnknownElement += Serializer_UnknownElement;

            var model = (StrictOrder)serializer.Deserialize(reader);

            Assert.AreEqual("What's in it?", model.Name);
            Assert.AreEqual(666, model.Value);
            Assert.AreEqual("extra", model.Extra);
        }

        private void Serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            
        }

        private void onUnknownNode(object sender, XmlNodeEventArgs e)
        {
           
        }
    }

    public class StrictOrder
    {
        [XmlElement(Order = 1)]
        public int Value { get; set; }
        
        [XmlElement(Order = 2)]
        public string Name { get; set; }

        [XmlElement(Order = 3)]
        public string Extra { get; set; }
    }
}
