using Ivory.UnitTests.TestTools;
using Ivory.Xml;
using NUnit.Framework;
using System;
using System.Xml;
using System.Xml.Linq;

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
    }
}
