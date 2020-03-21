namespace Ivory.Soap
{
    public static class SoapVersions
    {
        /// <summary>Gets the default namespace used for the SOAP version.</summary>
        public static string DefaultNamespace(this SoapVersion version)
        {
            return version switch
            {
                SoapVersion.None => string.Empty,
                SoapVersion.V1_1 => "http://schemas.xmlsoap.org/soap/envelope/",
                _ => "http://www.w3.org/2003/05/soap-envelope",
            };
        }
    }
}
