using System;

namespace Ivory.Soap
{
    /// <summary>Represents a SOAP action method.</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class SoapActionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoapActionAttribute"/> class.
        /// </summary>
        /// <param name="action">
        /// The SOAP action.
        /// </param>
        /// <param name="reply">
        /// The SOAP reply action.
        /// </param>
        public SoapActionAttribute(string action, string reply)
        {
            Action = action;
            Reply = reply;
        }

        /// <summary>Gets the SOAP action.</summary>
        public string Action { get; }

        /// <summary>Gets the SOAP reply action.</summary>
        public string Reply { get; }

        /// <inheritdoc/>
        public override string ToString() => $"Action: {Action}, Reply: {Reply}";
    }
}
