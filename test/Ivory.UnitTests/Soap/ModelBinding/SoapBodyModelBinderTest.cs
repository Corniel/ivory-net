using Ivory.Soap.Modelbinding;
using Ivory.TestModels;
using Ivory.UnitTests.Mocking;
using Ivory.UnitTests.TestTools;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Ivory.UnitTests.Soap.ModelBinding
{
    public class SoapBodyModelBinderTest
    {
        [Test]
        public async Task BindModelAsync_SimpleBodies_Success()
        {
            var context = ModelBindingContextStub.Create
            (
                fieldName: "body",
                modelType: typeof(SimpleBody[]),
                requestBody: Message.Embedded("SimpleBodies.xml")
            );

            var binder = new SoapBodyModelBinder();
            await binder.BindModelAsync(context);

            var body = ModelBindAssert.Success<SimpleBody[]>(context.Result);

            Assert.AreEqual(new[] { 42, 314, 666 }, body.Select(b => b.Value));
        }

        [Test]
        public async Task BindModelAsync_SimpleBody_Success()
        {
            var context = ModelBindingContextStub.Create
            (
                fieldName: "body",
                modelType: typeof(SimpleBody),
                requestBody: Message.Embedded("SimpleBody.xml")
            );

            var binder = new SoapBodyModelBinder();
            await binder.BindModelAsync(context);

            var body = ModelBindAssert.Success<SimpleBody>(context.Result);
            Assert.AreEqual(42, body.Value);
        }
    }
}
