namespace Ivory.Soap.Mvc
{
    /// <summary>Represents a SOAP Controller.</summary>
    public interface ISoapController
    {
        /// <summary>Gets the preferred <see cref="SoapWriterSettings"/> for the SOAP controller.</summary>
        SoapWriterSettings WriterSettings { get; }
    }
}
