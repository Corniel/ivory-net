using Ivory.Soap.UnitTests.TestTools;
using Ivory.SoapApi.Models;
using NUnit.Framework;
using System;

namespace Ivory.Soap.UnitTests
{
    public class SoapMessageTest
    {
        [Test]
        public void Save_NoHeader_ValidSoapV1_1()
        {
            var expected = Message.EmbeddedText("NoHeader_v1.1.xml");
            Console.WriteLine(expected);
            Console.WriteLine();

            using var context = WriterContext.Create(new SoapWriterSettings());

            var envelope = SoapEnvelope.New(new SimpleBody { Value = 17 });
            envelope.Save(context.Stream, context.Settings);

            Console.WriteLine(context.Content);
            

            Assert.AreEqual(expected, context.Content);
        }
    }
}
