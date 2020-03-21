using System;
using System.Xml.Serialization;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Represents a biding error.</summary>
    /// <remarks>
    /// Microsoft's flavor does not support XML serialization.
    /// </remarks>
    [Serializable]
    [XmlRoot(Namespace = "http://ivory.org/binding-error")]
    public class BindingError
    {
        /// <summary>Initializes a new instance of the <see cref="BindingError"/> class.</summary>
        public BindingError() { }

        /// <summary>Initializes a new instance of the <see cref="BindingError"/> class.</summary>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <param name="message">
        /// The binding error message.
        /// </param>
        public BindingError(string fieldName, string message) : this()
        {
            FieldName = fieldName;
            Message = message;
        }

        /// <summary>Gets and sets the field name.</summary>
        public string FieldName { get; set; }

        /// <summary>Gets and sets the binding error message.</summary>
        public string Message { get; set; }
    }
}
