using Ivory.Soap;
using Ivory.Soap.SoapApi.Models;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ivory.SoapApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class WithHeaderController : ControllerBase
    {
        [SoapAction(action: "http://ivory.net/with-header", reply: "http://ivory.net/with-header/reply")]
        public IActionResult WithHeader(SimpleHeader header, SimpleBody body)
        {
            body.Value++;
            return new SoapResult(header: header, body: body);
        }
    }
}
