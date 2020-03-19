using Ivory.Soap.Mvc;
using Ivory.Soap.UnitTests.TestTools;
using Ivory.SoapApi.Models;
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

            await ActionResultAssert.WritesToBody(@"<Envelope p1:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:p1=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
  <Body>
    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
      <Value>17</Value>
    </SimpleBody>
  </Body>
</Envelope>", result);
        }

        [Test]
        public async Task SoapResult_WithHeaderAndBody()
        {
            var result = new SoapResult(header: new SimpleHeader { Message = "Hello" }, body: new SimpleBody { Value = 17 });

            await ActionResultAssert.WritesToBody(@"<Envelope p1:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:p1=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
  <Header>
    <SimpleHeader xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
      <Message>Hello</Message>
    </SimpleHeader>
  </Header>
  <Body>
    <SimpleBody xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns="""">
      <Value>17</Value>
    </SimpleBody>
  </Body>
</Envelope>", result);
        }
    }
}
