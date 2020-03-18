using Ivory.Soap.UnitTests.Mocking;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ivory.Soap.UnitTests.TestTools
{
    public static class ActionResultAssert
    {
        public static async Task WritesToBody(string expected, IActionResult actionResult)
        {
            var stream = new MemoryStream();
            var context = new ActionContextStub();
            context.HttpContext.Response.Body = stream;

            await actionResult.ExecuteResultAsync(context);

            var actual = Encoding.UTF8.GetString(stream.ToArray());

            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
    }
}
