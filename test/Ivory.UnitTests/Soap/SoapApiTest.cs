using Ivory.Soap;
using Ivory.Soap.Http;
using Ivory.SoapApi;
using Ivory.TestModels;
using Ivory.UnitTests.TestTools;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivory.UnitTests.Soap
{
    public class SoapApiTest : WebApplicationFactory<Startup>
    {
        [Test]
        public async Task Post_WithABody_GetsRepsonse()
        {
            using var client = CreateClient();

            var envelope = SoapEnvelope.New(new SimpleBody { Value = 16 });

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/without-header",
                envelope: envelope,
                cancellationToken: default);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            var content = SoapEnvelope.Load<SimpleBody>(await response.Content.ReadAsStreamAsync());

            Assert.AreEqual(17, content.Body.FirstOrDefault()?.Value);
        }

        [Test]
        public async Task Post_NoBody_SoapFault()
        {
            using var client = CreateClient();

            var envelope = SoapEnvelope.New<SimpleHeader, SimpleBody>(new SimpleHeader(), null);

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/with-header",
                envelope: envelope,
                cancellationToken: default);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            var content = SoapEnvelope.Load<SoapFault>(await response.Content.ReadAsStreamAsync());
            SoapResponseAssert.Fault(SoapFaultCode.Client, "The SOAP body node is missing.", content);
        }

        [Test]
        public async Task Post_InvalidXml_SoapFault()
        {
            using var client = CreateClient();
            client.DefaultRequestHeaders.Add("SOAPAction", "http://ivory.net/with-header");

            var envelope = new StringContent("#<Envelope />");

            var response = await client.PostAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                content: envelope);

            var content = SoapEnvelope.Load<SoapFault>(await response.Content.ReadAsStreamAsync());
            SoapResponseAssert.Fault(SoapFaultCode.Client, "Data at the root level is invalid. Line 1, position 1.", content);
        }


        [Test]
        public async Task CallActtion_IvoryNetException_ReturnsSoapFault()
        {
            using var client = CreateClient();
            var envelope = SoapEnvelope.New(new SimpleBody { Value = 666 });

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/exception",
                envelope: envelope,
                cancellationToken: default);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            var content = SoapEnvelope.Load<SoapFault>(await response.Content.ReadAsStreamAsync());
            SoapResponseAssert.Fault(SoapFaultCode.Server, "Attempted to divide by zero.", content);
        }
    }
}
