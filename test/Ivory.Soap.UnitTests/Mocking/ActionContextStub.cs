using Microsoft.AspNetCore.Mvc;

namespace Ivory.Soap.UnitTests.Mocking
{
    public class ActionContextStub : ActionContext
    {
        public ActionContextStub()
        {
            HttpContext = new HttpContextStub();
        }
    }
}
