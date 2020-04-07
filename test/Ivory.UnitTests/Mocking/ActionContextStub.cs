using Microsoft.AspNetCore.Mvc;

namespace Ivory.UnitTests.Mocking
{
    public class ActionContextStub : ActionContext
    {
        public ActionContextStub()
        {
            HttpContext = new HttpContextStub();
        }
    }
}
