using Ivory.Soap.UnitTests.Models;
using Ivory.Soap.UnitTests.TestTools;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Ivory.Soap.UnitTests
{
    public class SoapResultTest
    {
        [Test]
        public async Task SoapResult_WithBody()
        {
            var result = new SoapResult(header: null, body: new SimpleBody { Value = 17 });

            await ActionResultAssert.WritesToBody(@"<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=""http://www.w3.org/2001/12/soap-encoding"" xmlns:SOAP-ENV=""http://www.w3.org/2001/12/soap-envelope"">
  <SOAP-ENV:Body>
    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
      <Value>17</Value>
    </SimpleBody>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>", result);
        }

        [Test]
        public async Task SoapResult_WithHeaderAndBody()
        {
            var result = new SoapResult(header: new SimpleHeader { Message = "Hello" }, body: new SimpleBody { Value = 17 });

            await ActionResultAssert.WritesToBody(@"<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=""http://www.w3.org/2001/12/soap-encoding"" xmlns:SOAP-ENV=""http://www.w3.org/2001/12/soap-envelope"">
  <SOAP-ENV:Header>
    <SimpleHeader xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
      <Message>Hello</Message>
    </SimpleBody>
  </SOAP-ENV:Header>
  <SOAP-ENV:Body>
    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
      <Value>17</Value>
    </SimpleBody>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>", result);
        }
    }
}
