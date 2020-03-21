using Ivory.Soap.Modelbinding;
using Ivory.Soap.UnitTests.Mocking;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace Ivory.Soap.UnitTests.ModelBinding
{
    public class SoapModelBinderProviderTest
    {
        [TestCase(typeof(SoapEnvelopeModelBinder), "envelope")]
        [TestCase(typeof(SoapHeaderModelBinder), "header")]
        [TestCase(typeof(SoapBodyModelBinder), "body")]
        [TestCase(null, "other")]
        public void GetBinder(Type expected, string parameterName)
        {
            var provider = new SoapModelBinderProvider();

            var parameter = new ParameterInfoStub
            (
                name: parameterName,
                parameterType: typeof(XElement)
            );

            var identity = ModelMetadataIdentity.ForParameter(parameter);
            var metadata = new ModelMetadataStub(identity);
            var context = new ModelBinderProviderContextStub(metadata);
            var binder = provider.GetBinder(context);

            Assert.AreEqual(expected, binder?.GetType());
        }
    }
}
