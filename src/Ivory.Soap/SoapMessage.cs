namespace Ivory.Soap
{
    /// <summary>Helper method for SOAP message literals.</summary>
    public static class SoapMessage
    {
        /// <summary>Gets the SOAPAction HTTP Header.</summary>
        public static readonly string SOAPAction = nameof(SOAPAction);

        /// <summary>Gets the SOAP 1.1 envelope namespace.</summary>
        public static readonly string NsSoap11Envelop = "http://schemas.xmlsoap.org/soap/envelope/";

        /// <summary>Gets the SOAP 1.2 envelope namespace.</summary>
        public static readonly string NsSoap12Envelop = "http://www.w3.org/2003/05/soap-envelope";

        /// <summary>Gets the SOAP 1.1 message encoding style.</summary>
        public static readonly string EncodingStyleSoap11 = "http://schemas.xmlsoap.org/soap/encoding/";

        /// <summary>Gets the SOAP 1.2 message encoding style.</summary>
        public static readonly string EncodingStyleSoap12 = "http://www.w3.org/2003/05/soap-encoding";

        /// <summary>Gets the SOAP envelope.</summary>
        public static readonly string Envelope = nameof(Envelope);

        /// <summary>Gets the SOAP header.</summary>
        public static readonly string Header = nameof(Header);

        /// <summary>Gets the SOAP body.</summary>
        public static readonly string Body = nameof(Body);

        /// <summary>Gets the SOAP fault.</summary>
        public static readonly string Fault = nameof(Fault);
    }
}
