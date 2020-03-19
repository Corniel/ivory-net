using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Implementation of an <see cref="IModelBinder"/> for SOAP.</summary>
    public class SoapModelBinder : IModelBinder, IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return this;
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            // This binder is only suited for SOAP requests.
            if (!bindingContext.HttpContext.IsSoapRequest())
            {
                return Task.CompletedTask;
            }

            var isHeader = bindingContext.ModelName == "header";
            var reader = XmlReader.Create(bindingContext.HttpContext.Request.Body, ReaderSettings);

            reader.ReadToFollowing(isHeader ? SoapMessage.Header : SoapMessage.Body, SoapMessage.NS);

            try
            {
                if (typeof(IXmlSerializable).IsAssignableFrom(bindingContext.ModelType))
                {
                    var xmlSerializable = (IXmlSerializable)Activator.CreateInstance(bindingContext.ModelType);
                    xmlSerializable.ReadXml(reader);
                    bindingContext.Result = ModelBindingResult.Success(xmlSerializable);
                }
                else
                {
                    var serializer = new XmlSerializer(bindingContext.ModelType);
                    bindingContext.Result = ModelBindingResult.Success(serializer.Deserialize(reader));
                }
            }
            catch (Exception x)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, x.Message);
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return Task.CompletedTask;
        }

        private static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings
        {
            Async = true,
            CloseInput = false,
            IgnoreComments = true,
            IgnoreWhitespace = true,
            IgnoreProcessingInstructions = true,
            CheckCharacters = false,
            ConformanceLevel = ConformanceLevel.Fragment,
            ValidationFlags = XmlSchemaValidationFlags.None,
        };
    }
}
