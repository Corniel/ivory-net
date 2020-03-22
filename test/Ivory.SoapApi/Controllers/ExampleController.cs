using Ivory.Soap;
using Ivory.Soap.Mvc;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace Ivory.SoapApi.Controllers
{
    [SoapController(version: SoapVersion.V1_1)]
    public class ExampleSoapController : ControllerBase
    {
        [SoapAction("http://ivory.net/with-header")]
        public IActionResult WithHeader(SimpleHeader header, SimpleBody body)
        {
            body.Value++;
            return this.Soap(header, body);
        }

        [SoapAction("http://ivory.net/without-header")]
        public IActionResult WithoutHeader(SimpleBody body)
        {
            body.Value++;
            return this.Soap(body);
        }

        [SoapAction("http://ivory.net/xml-withHeader")]
        public IActionResult XmlWithHeader(XElement header, XElement body)
        {
            return this.Soap(header: header, body: body);
        }

        [SoapAction("http://ivory.net/xml")]
        public IActionResult Xml(XElement body)
        {
            return this.Soap(body);
        }

        [SoapAction("http://ivory.net/exception")]
        public IActionResult Xml(SimpleBody body)
        {
            throw new DivideByZeroException();
        }
    }
}
