using Ivory.Soap.Mvc;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Ivory.SoapApi.Controllers
{
    [SoapV1_1]
    [Route("/")]
    public class WithHeaderController : ControllerBase
    {
        [SoapAction("http://ivory.net/with-header")]
        public IActionResult WithHeader(SimpleHeader header, SimpleBody body)
        {
            body.Value++;
            return this.Soap(header: header, body: body);
        }

        [SoapAction("http://ivory.net/without-header")]
        public IActionResult WithoutHeader(SimpleBody body)
        {
            body.Value++;
            return this.Soap(body: body);
        }

        [SoapAction("http://ivory.net/xml")]
        public IActionResult Xml(XElement body)
        {
            return this.Soap(body: body);
        }
    }
}
