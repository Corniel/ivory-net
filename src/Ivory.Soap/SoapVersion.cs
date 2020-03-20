#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
#pragma warning disable SA1310 // Field names should not contain underscore
// The most logical way of sprecifying SOAP versions is as v1_1 etc.

using System;
using System.Xml.Linq;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP version.</summary>
    public readonly struct SoapVersion : IEquatable<SoapVersion>
    {
        /// <summary>No SOAP version.</summary>
        public static readonly SoapVersion None;

        /// <summary>SOAP v1.1: (http://schemas.xmlsoap.org/soap/envelope/).</summary>
        public static readonly SoapVersion v1_1 = new SoapVersion("1.1", "http://schemas.xmlsoap.org/soap/envelope/");

        /// <summary>SOAP v1.2: (http://www.w3.org/2003/05/soap-envelope).</summary>
        public static readonly SoapVersion v1_2 = new SoapVersion("1.2", "http://www.w3.org/2003/05/soap-envelope");

        /// <summary>Initializes a new instance of the <see cref="SoapVersion"/> struct.</summary>
        /// <param name="version">
        /// The SOAP version.
        /// </param>
        /// <param name="namespace">
        /// The SOAP version namespace.
        /// </param>
        public SoapVersion(string version, string @namespace)
        {
            Version = version;
            Namespace = @namespace;
        }

        /// <summary>Gets the SOAP version.</summary>
        public string Version { get; }

        /// <summary>Gets the SOAP version namespace.</summary>
        public string Namespace { get; }

        /// <summary>Gets the <see cref="XNamespace"/> representing the SOAP version.</summary>
        public XNamespace XNamespace => XNamespace.Get(Namespace ?? string.Empty);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SoapVersion other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(SoapVersion other)
        {
            return Version == other.Version
                && Namespace == other.Namespace;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Version is null ? 0 : Version.GetHashCode())
                ^ (Namespace is null ? 0 : Namespace.GetHashCode());
        }

        /// <summary>Returns true if the two SOAP version are equal.</summary>
        public static bool operator ==(SoapVersion l, SoapVersion r) => l.Equals(r);

        /// <summary>Returns true if the two SOAP version are equal.</summary>
        public static bool operator !=(SoapVersion l, SoapVersion r) => !(l == r);
    }
}
