using Ivory.Soap.Modelbinding;
using Ivory.UnitTests.Mocking;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace Ivory.UnitTests.Soap.ModelBinding
{
    public class SoapModelBinderProviderTest
    {
        [Test]
        public void GetBinder_SoapEnvelopeBindingSource_SoapHeaderModelBinder()
        {
            AssertModelBinider(typeof(SoapEnvelopeModelBinder), SoapBindingSource.SoapEnvelope);
        }

        [Test]
        public void GetBinder_SoapHeaderBindingSource_SoapHeaderModelBinder()
        {
            AssertModelBinider(typeof(SoapHeaderModelBinder), SoapBindingSource.SoapHeader);
        }

        [Test]
        public void GetBinder_SoapBodyBindingSource_SoapHeaderModelBinder()
        {
            AssertModelBinider(typeof(SoapBodyModelBinder), SoapBindingSource.SoapBody);
        }

        [Test]
        public void GetBinder_BodyBindingSource_IsNull()
        {
            AssertModelBinider(null, BindingSource.Body);
        }

        [Test]
        public void GetBinder_NullBindingSource_IsNull()
        {
            AssertModelBinider(null, BindingSource.Body);
        }


        private static void AssertModelBinider(Type expected, BindingSource bindingSource)
        {
            var provider = new SoapModelBinderProvider();

            var parameter = new ParameterInfoStub
            (
                name: "param",
                parameterType: typeof(XElement)
            );

            var identity = ModelMetadataIdentity.ForParameter(parameter);
            var metadata = new ModelMetadataStub(identity, bindingSource: bindingSource);
            var context = new ModelBinderProviderContextStub(metadata);
            var binder = provider.GetBinder(context);

            Assert.AreEqual(expected, binder?.GetType());
        }
    }
}
