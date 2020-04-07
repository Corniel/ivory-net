using Ivory.Soap;
using NUnit.Framework;
using System.Linq;

namespace Ivory.UnitTests.TestTools
{
    public static class SoapResponseAssert
    {
        public static void Fault(SoapFaultCode faultCode, string faultString, SoapEnvelope<SoapFault> actual)
        {
            var fault = actual.Body.Single();

            Assert.AreEqual(faultCode, fault.FaultCode);
            Assert.AreEqual(faultString, fault.FaultString);
        }
    }
}
