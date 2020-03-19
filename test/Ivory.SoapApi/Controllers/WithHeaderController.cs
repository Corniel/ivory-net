using Ivory.Soap;
using Ivory.Soap.SoapApi.Models;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ivory.SoapApi.Controllers
{
    [Route("/")]
    public class WithHeaderController : ControllerBase
    {
        [SoapAction("http://ivory.net/with-header")]
        public IActionResult WithHeader(SimpleHeader header, SimpleBody body)
        {
            body.Value++;
            return new SoapResult(header: header, body: body);
        }

        [SoapAction("http://ivory.net/without-header")]
        public IActionResult WithoutHeader(SimpleBody body)
        {
            body.Value++;
            return new SoapResult(header: null, body: body);
        }
    }
}
