using Ivory.Soap.Modelbinding;
using Ivory.Soap.UnitTests.Mocking;
using Ivory.Soap.UnitTests.TestTools;
using Ivory.SoapApi.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Ivory.Soap.UnitTests.ModelBinding
{
    public class SoapHeaderModelBinderTest
    {
        [Test]
        public async Task BindModelAsync_SimpleHeader_Success()
        {
            var context = ModelBindingContextStub.Create
            (
                fieldName: "header",
                modelType: typeof(SimpleHeader),
                requestBody: Message.Embedded("SimpleHeader.xml")
            );

            var binder = new SoapHeaderModelBinder();
            await binder.BindModelAsync(context);

            var header = ModelBindAssert.Success<SimpleHeader>(context.Result);
            Assert.AreEqual("Ivory.NET", header.Message);
        }
    }
}
