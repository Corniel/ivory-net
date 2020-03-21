using Ivory.Soap.Http;
using Ivory.SoapApi;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.UnitTests
{
    public class SoapApiTest : WebApplicationFactory<Startup>
    {

        [Test]
        public async Task CallAction_NoBody_SoapFault()
        {
            using var client = CreateClient();

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/with-header",
                header: null,
                body: new SimpleHeader(),
                cancellationToken: default);

            foreach(var header in response.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            //var message = await SoapMessage.LoadAsync(await response.Content.ReadAsStreamAsync(), typeof(XElement), typeof(SimpleBody));

            //var actual = (SimpleBody)message.Body;

            //Assert.AreEqual(17, actual.Value);
        }

        [Test]
        public async Task CallAction_WithABody_GetsRepsonse()
        {
            using var client = CreateClient();

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/without-header",
                header: null,
                body: new SimpleBody { Value = 16 },
                cancellationToken: default);

            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            var message =await SoapMessage.LoadAsync(await response.Content.ReadAsStreamAsync(), typeof(XElement), typeof(SimpleBody));

            var actual = (SimpleBody)message.Body;

            Assert.AreEqual(17, actual.Value);
        }


        [Test]
        public async Task CallActtion_IvoryNetException_ReturnsSoapFault()
        {
            using var client = CreateClient();

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/exception",
                header: null,
                body: new SimpleBody { Value = 666 },
                cancellationToken: default);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            //var message = await SoapMessage.LoadAsync(await response.Content.ReadAsStreamAsync(), typeof(XElement), typeof(SimpleBody));

            //var actual = (SimpleBody)message.Body;

            //Assert.AreEqual(17, actual.Value);
        }
    }
}
