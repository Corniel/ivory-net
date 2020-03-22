//using Ivory.Soap.UnitTests.TestTools;
//using Ivory.SoapApi.Models;
//using NUnit.Framework;
//using System;
//using System.IO;
//using System.Threading.Tasks;

//namespace Ivory.Soap.UnitTests
//{
//    public class SoapMessageTest
//    {
//        [Test]
//        public async Task LoadAsync_NoEnvelope_LoadedSuccessfully()
//        {
//            var actual = await SoapMessage.LoadAsync(GetMessageStream("NoEvenlope.xml"));
//            Assert.NotNull(actual);
//            Assert.NotNull(actual.Header, "Header");
//            Assert.NotNull(actual.Body, "Body");
//        }

//        [Test]
//        public async Task LoadAsync_HeaderWithOtherNamespace_HeaderIsIgnored()
//        {
//            var actual = await SoapMessage.LoadAsync(GetMessageStream("HeaderWithOtherNamespace.xml"));
//            Assert.NotNull(actual);
//            Assert.IsNull(actual.Header, "Header");
//            Assert.NotNull(actual.Body, "Body");
//        }

//        [Test]
//        public void Save_CustomPrefixAndVersion()
//        {
//            var settings = new SoapWriterSettings
//            { 
//                NamespacePrefix = "soap",
//                SoapVersion = SoapVersion.None,
//                Namespace = "http://ivory.org/soap",
//            };

//            using var context = WriterContext.Create(settings);

//            var message = new SoapMessage(header: null, body: new SimpleBody { Value = 66 });
//            message.Save(context.Writer, context.Settings);

//            Console.WriteLine(context.Content);

//            Assert.AreEqual(@"<soap:Envelope xmlns:soap=""http://ivory.org/soap"">
//  <soap:Body>
//    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
//      <Value>66</Value>
//    </SimpleBody>
//  </soap:Body>
//</soap:Envelope>", context.Content);
//        }

//        [Test]
//        public void Save_NoHeader_ValidSoapV1_1()
//        {
//            using var context = WriterContext.Create(SoapWriterSettings.V1_1);

//            var message = new SoapMessage(header: null, body: new SimpleBody { Value = 17 });
//            message.Save(context.Writer, context.Settings);

//            Console.WriteLine(context.Content);

//            Assert.AreEqual(@"<Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
//  <Body>
//    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
//      <Value>17</Value>
//    </SimpleBody>
//  </Body>
//</Envelope>", context.Content);
//        }

//        [Test]
//        public void Save__WithHeaderAndBody_ValidSoapV1_2()
//        {
//            using var context = WriterContext.Create(SoapWriterSettings.V1_2);

//            var message = new SoapMessage(header: new SimpleHeader { Message = "Hello" }, body: new SimpleBody { Value = 17 });
//            message.Save(context.Writer, context.Settings);

//            Console.WriteLine(context.Content);

//            Assert.AreEqual(@"<Envelope xmlns=""http://www.w3.org/2003/05/soap-envelope"">
//  <Header>
//    <SimpleHeader xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
//      <Message>Hello</Message>
//    </SimpleHeader>
//  </Header>
//  <Body>
//    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
//      <Value>17</Value>
//    </SimpleBody>
//  </Body>
//</Envelope>", context.Content);
//        }

//        private Stream GetMessageStream(string name)
//        {
//            return GetType().Assembly.GetManifestResourceStream($"Ivory.Soap.UnitTests.Messages.{name}");
//        }
//    }
//}
