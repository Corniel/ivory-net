using Ivory.Soap.UnitTests.TestTools;
using NUnit.Framework;
using System;
using System.Xml.Serialization;

namespace Ivory.Soap.UnitTests
{
    public class XmlSerialize
    {
        [Test]
        public void Serialize()
        {
            using var context = WriterContext.Create(SoapWriterSettings.V1_1);

            var envelope = new SoapEnvelope<MyHeader, MyBody>
            {
                //Header = new SoapHeader<MyHeader>
                //{
                //    Details = new[] { new MyHeader() },
                //},
                Body = new SoapBody<MyBody>
                {
                    new MyBody(), 
                    new MyBody { Message = "Extra" },
                }
            };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://schemas.xmlsoap.org/soap/envelope/");

            var serializer = new XmlSerializer(envelope.GetType());
            serializer.Serialize(context.Writer, envelope, ns);

            Console.WriteLine(context.Content);
        }

        [Serializable]
        public class MyHeader
        {
            public string Message { get; set; } = "Header";
        }

        [Serializable]
        public class MyBody
        {
            public string Message { get; set; } = "Body";
        }

    }
}
