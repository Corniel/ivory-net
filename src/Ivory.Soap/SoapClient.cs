using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ivory.Soap
{
    public static class SoapClient
    {
        public static readonly string SOAPAction = nameof(SOAPAction);

        public static Task<HttpResponseMessage> PostSoapAsync(
            this HttpClient httpClient,
            Uri requestUri,
            string soapAction,
            object header,
            object body,
            CancellationToken cancellationToken)
        {
            Guard.NotNull(httpClient, nameof(httpClient));

            httpClient.DefaultRequestHeaders.Add(SOAPAction, soapAction);
            var content = new SoapHttpContent(header, body);
            return httpClient.PostAsync(requestUri, content, cancellationToken);
        }
    }
}
