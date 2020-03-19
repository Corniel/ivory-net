using Ivory.SoapApi;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Ivory.Soap.UnitTests
{
    public class SoapApiTest : WebApplicationFactory<Startup>
    {
        [Test]
        public async Task PostSoapAsync()
        {
            using var client = CreateClient();

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/with-header",
                header: null,
                body: new SimpleBody(),
                cancellationToken: default);
        }
    }
}
