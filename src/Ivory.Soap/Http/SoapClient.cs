using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ivory.Soap.Http
{
    /// <summary>Extensions for SOAP requests on <see cref="HttpClient"/>.</summary>
    public static class SoapClient
    {
        /// <summary>Send a SOAP request via HTTP POST as a asynchronous operation.</summary>
        /// <param name="httpClient">
        /// The <see cref="HttpClient"/> to use.
        /// </param>
        /// <param name="requestUri">
        /// The <see cref="Uri"/> of the SOAP endpoint.
        /// </param>
        /// <param name="soapAction">
        /// The SOAP action to apply.
        /// </param>
        /// <param name="header">
        /// The (optional) SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public static Task<HttpResponseMessage> PostSoapAsync(
           this HttpClient httpClient,
           Uri requestUri,
           string soapAction,
           object header,
           object body)
        {
            return httpClient.PostSoapAsync(requestUri, soapAction, header, body, default);
        }

        /// <summary>
        /// Send a SOAP request via HTTP POST with a cancellation token as an
        /// asynchronous operation.
        /// </summary>
        /// <param name="httpClient">
        /// The <see cref="HttpClient"/> to use.
        /// </param>
        /// <param name="requestUri">
        /// The <see cref="Uri"/> of the SOAP endpoint.
        /// </param>
        /// <param name="soapAction">
        /// The SOAP action to apply.
        /// </param>
        /// <param name="header">
        /// The (optional) SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used by other objects or threads to receive
        /// notice of cancellation.
        /// </param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public static Task<HttpResponseMessage> PostSoapAsync(
            this HttpClient httpClient,
            Uri requestUri,
            string soapAction,
            object header,
            object body,
            CancellationToken cancellationToken)
        {
            Guard.NotNull(httpClient, nameof(httpClient));

            httpClient.DefaultRequestHeaders.Add(SoapMessage.SOAPAction, soapAction);
            var content = new SoapHttpContent(header, body);
            return httpClient.PostAsync(requestUri, content, cancellationToken);
        }
    }
}
