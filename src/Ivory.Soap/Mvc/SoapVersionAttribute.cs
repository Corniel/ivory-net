using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Ivory.Soap.Mvc
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SoapVersionAttribute : Attribute, IActionFilter
    {
        public SoapVersionAttribute(string version, string @namespace)
        {
            Version = new SoapVersion(version, @namespace);
        }

        public SoapVersion Version { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public override string ToString() => Version.ToString();

        internal SoapWriterSettings GetSettings()
        {
            return new SoapWriterSettings
            { 
                SoapVersion = Version,
            };

        }
    }

    public sealed class SoapV1_1Attribute : SoapVersionAttribute
    {
        public SoapV1_1Attribute() : base(SoapVersion.v1_1.Version, SoapVersion.v1_1.Namespace) { }
    }

    public sealed class SoapV1_2Attribute : SoapVersionAttribute
    {
        public SoapV1_2Attribute() : base(SoapVersion.v1_2.Version, SoapVersion.v1_2.Namespace) { }
    }
}
