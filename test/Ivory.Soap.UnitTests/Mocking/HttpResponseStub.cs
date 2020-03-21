using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ivory.Soap.UnitTests.Mocking
{
    public class HttpResponseStub : HttpResponse
    {
        public override HttpContext HttpContext => throw new NotSupportedException();

        public override int StatusCode { get; set; }

        public override IHeaderDictionary Headers => throw new NotSupportedException();

        public override Stream Body { get; set; } = new MemoryStream();
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }

        public override IResponseCookies Cookies => throw new NotSupportedException();

        public override bool HasStarted => throw new NotSupportedException();

        public override void OnCompleted(Func<object, Task> callback, object state) => throw new NotSupportedException();

        public override void OnStarting(Func<object, Task> callback, object state) => throw new NotSupportedException();
        
        public override void Redirect(string location, bool permanent) => throw new NotSupportedException();
    }
}
