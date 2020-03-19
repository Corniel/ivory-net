using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Implementation of an <see cref="IModelBinder"/> for SOAP.</summary>
    public class SoapModelBinder : IModelBinder, IModelBinderProvider
    {
        private static readonly XNamespace NsSoap11Envelop = XNamespace.Get(SoapMessage.NsSoap11Envelop);
        private static readonly XNamespace NsSoap12Envelop = XNamespace.Get(SoapMessage.NsSoap12Envelop);
        private static readonly string envelope = nameof(envelope);
        private static readonly string header = nameof(header);
        private static readonly string body = nameof(body);

        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return this;
        }

        /// <inheritdoc/>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            // This binder is only suited for SOAP requests.
            if (!bindingContext.HttpContext.IsSoapRequest())
            {
                return;
            }

            var isHeader = bindingContext.FieldName == header;

            if (!isHeader && bindingContext.FieldName != body)
            {
                return;
            }

            var xEnvelope = await GetSoapEnvelopeAsync(bindingContext);

            if (xEnvelope is null)
            {
                return;
            }

            var xContent = isHeader
                ? GetSoapHeader(xEnvelope)
                : GetSoapBody(xEnvelope);

            object model;

            try
            {
                model = Deserialize(xContent, bindingContext.ModelType);
            }
            catch (Exception x)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, x.Message);
                return;
            }

            if (!isHeader && model is null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "SOAP body is missing.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }
            bindingContext.Result = ModelBindingResult.Success(model);
        }

        private static async Task<XElement> GetSoapEnvelopeAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelState.TryGetValue(envelope, out var entry))
            {
                return (XElement)entry.RawValue;
            }

            var stream = bindingContext.HttpContext.Request.Body;
            try
            {
                var root = await XElement.LoadAsync(stream, LoadOptions.PreserveWhitespace, default);
                bindingContext.ModelState.SetModelValue(envelope, root, string.Empty);
                return root;
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return null;
        }

        private static XElement GetSoapHeader(XElement soapRequest)
        {
            Guard.NotNull(soapRequest, nameof(SaveOptions));
            var soapHeader = soapRequest.Element(NsSoap11Envelop + SoapMessage.Header)
                          ?? soapRequest.Element(NsSoap12Envelop + SoapMessage.Header);
            return soapHeader?.Elements().FirstOrDefault();
        }

        private static XElement GetSoapBody(XElement soapRequest)
        {
            Guard.NotNull(soapRequest, nameof(SaveOptions));
            var soapBody = soapRequest.Element(NsSoap11Envelop + SoapMessage.Body)
                        ?? soapRequest.Element(NsSoap12Envelop + SoapMessage.Body);
            return soapBody.Elements().FirstOrDefault();
        }

        private static object Deserialize(XNode node, Type type)
        {
            if (node is null)
            {
                return null;
            }
            if (node.GetType() == type)
            {
                return node;
            }
            var reader = node.CreateReader();

            if (typeof(IXmlSerializable).IsAssignableFrom(type))
            {
                var xmlSerializable = (IXmlSerializable)Activator.CreateInstance(type);
                xmlSerializable.ReadXml(reader);
                return xmlSerializable;
            }
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(reader);
        }
    }
}
