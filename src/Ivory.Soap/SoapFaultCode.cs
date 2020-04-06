namespace Ivory.Soap
{
    /// <summary>The faultCode values defined below must be used in the faultcode element while describing faults.</summary>
    public enum SoapFaultCode
    {
        /// <summary>Found an invalid namespace for the SOAP Envelope element.</summary>
        VersionMismatch,

        /// <summary>An immediate child element of the Header element, with the mustUnderstand attribute set to "1", was not understood.</summary>
        MustUnderstand,

        /// <summary>The message was incorrectly formed or contained incorrect information.</summary>
        Client,

        /// <summary>There was a problem with the server, so the message could not proceed..</summary>
        Server,
    }
}
