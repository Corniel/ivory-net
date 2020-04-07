using Ivory.Soap;
using Ivory.TestModels;
using Ivory.UnitTests.TestTools;
using NUnit.Framework;
using System;

namespace Ivory.UnitTests.Soap
{
    public class SoapMessageTest
    {
        [Test]
        public void Save_AlternativeNamespacePrefix_ValidSoap()
        {
            var expected = Message.EmbeddedText("AlternativeNamespacePrefix.xml");
            Console.WriteLine(expected);
            Console.WriteLine();
            var settings = new SoapWriterSettings();
            settings.QualifiedNames.UpdatePrefix("xxx", "http://schemas.xmlsoap.org/soap/envelope/");

            using var context = WriterContext.Create(settings);

            var envelope = SoapEnvelope.New(new SimpleBody { Value = 17 });
            envelope.Save(context.Stream, context.Settings);

            Assert.AreEqual(expected, context.Content);
        }


        [Test]
        public void Save_NoHeader_ValidSoap()
        {
            var expected = Message.EmbeddedText("NoHeader_v1.1.xml");
            Console.WriteLine(expected);
            Console.WriteLine();

            using var context = WriterContext.Create(new SoapWriterSettings());

            var envelope = SoapEnvelope.New(new SimpleBody { Value = 17 });
            envelope.Save(context.Stream, context.Settings);

            Assert.AreEqual(expected, context.Content);
        }

        [Test]
        public void Save_Fault_ValidSoapV1_1()
        {
            var expected = Message.EmbeddedText("Fault_v1.1.xml");
            Console.WriteLine(expected);
            Console.WriteLine();

            using var context = WriterContext.Create(new SoapWriterSettings());

            var envelope = SoapEnvelope.Fault(
                new SoapFault(SoapFaultCode.Client, "Oops!")
                { 
                    FaultActor = "Not me",
                });

            envelope.Save(context.Stream, context.Settings);

            Assert.AreEqual(expected, context.Content);
        }

        [Test]
        public void Save_HeaderWithComplexBody_ValidSoapV1_1()
        {
            var expected = Message.EmbeddedText("HeaderWithComplexBody_v1.1.xml");
            Console.WriteLine(expected);
            Console.WriteLine();

            var settings = new SoapWriterSettings();
            settings.QualifiedNames
                .Add("complex", "http://ivory.net/compex")
                .Add("m", "http://ivory.net/compex-mood");

            using var context = WriterContext.Create(settings);

            var envelope = SoapEnvelope.New(
                header: new SimpleHeader
                {
                    Message = "Wash your hands!",
                },
                body: new ComplexBody
                {
                    Answer = 42,
                    Mood = "Mellow",
                });

            envelope.Save(context.Stream, context.Settings);

            Assert.AreEqual(expected, context.Content);
        }
    }
}
