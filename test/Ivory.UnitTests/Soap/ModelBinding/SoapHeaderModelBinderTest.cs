using Ivory.Soap.Modelbinding;
using Ivory.TestModels;
using Ivory.UnitTests.Mocking;
using Ivory.UnitTests.TestTools;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Ivory.UnitTests.Soap.ModelBinding
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
