using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>A metadata object representing a source of data for SOAP model binding.</summary>
    public class SoapBindingSource : BindingSource
    {
        /// <summary>A <see cref="SoapBindingSource"/> for the SOAP envelope.</summary>
        public static readonly SoapBindingSource SoapEnvelope = new SoapBindingSource(nameof(SoapEnvelope), "SOAP envelope");

        /// <summary>A <see cref="SoapBindingSource"/> for the SOAP header.</summary>
        public static readonly SoapBindingSource SoapHeader = new SoapBindingSource(nameof(SoapHeader), "SOAP header");

        /// <summary>A <see cref="SoapBindingSource"/> for the SOAP body.</summary>
        public static readonly SoapBindingSource SoapBody = new SoapBindingSource(nameof(SoapBody), "SOAP body");

        /// <summary>Initializes a new instance of the <see cref="SoapBindingSource"/> class.</summary>
        protected SoapBindingSource(string id, string displayName)
            : base(id, displayName, true, true) { }
    }
}
