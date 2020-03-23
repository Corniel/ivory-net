using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Abstract SOAP model binder.</summary>
    public abstract class SoapModelBinder : IModelBinder
    {
        /// <summary>Returns the supported <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource"/>.</summary>
        public abstract BindingSource BindingSource { get; }

        /// <inheritdoc/>
        public virtual async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            var container = await GetContainerAysnc(bindingContext);

            if (container is null)
            {
                // Invalid state.
            }
            else if (bindingContext.ModelType == typeof(XContainer))
            {
                BindXContainer(bindingContext, container);
            }
            else if (bindingContext.ModelType == typeof(XElement))
            {
                BindXElement(bindingContext, container);
            }
            else
            {
                BindXmlSerializable(bindingContext, container);
            }
        }

        /// <summary>Gets the <see cref="XContainer"/> for the part to bind.</summary>
        protected abstract Task<XContainer> GetContainerAysnc(ModelBindingContext bindingContext);

        /// <summary>Gets the SOAP envelope.</summary>
        /// <remarks>
        /// The is result is always stored in the model state.
        /// </remarks>
        protected static async Task<XDocument> GetEnvelopeAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            if (!bindingContext.ModelState.IsValid)
            {
                return null;
            }

            if (bindingContext.ModelState.TryGetValue("$envelope", out var entry))
            {
                return (XDocument)entry.RawValue;
            }

            var stream = bindingContext.HttpContext.Request.Body;
            try
            {
                var message = await XDocument.LoadAsync(stream, LoadOptions.None, default);

                if (message.Root.Name.LocalName != "Envelope")
                {
                    bindingContext.ModelState.AddModelError("envelope", SoapMessages.NoEnvelope);
                    return null;
                }
                if (message.Root.Element(message.Root.Name.Namespace + "Body").Elements()?.Any() != true)
                {
                    bindingContext.ModelState.AddModelError("envelope", SoapMessages.NoBody);
                    return null;
                }
                bindingContext.ModelState.SetModelValue("$envelope", message, string.Empty);
                return message;
            }
#pragma warning disable S2221 // "Exception" should not be caught when not required by called methods
            // Exception is used for handling errors.
            catch (Exception x)
#pragma warning restore S2221 // "Exception" should not be caught when not required by called methods
            {
                bindingContext.ModelState.AddModelError("envelope", x.Message);
            }
            return null;
        }

        private static void BindXContainer(ModelBindingContext bindingContext, XContainer container)
        {
            bindingContext.Result = ModelBindingResult.Success(container);
        }

        private static void BindXElement(ModelBindingContext bindingContext, XContainer container)
        {
            var second = container.Elements().Skip(1).FirstOrDefault();

            if (second is null)
            {
                bindingContext.Result = ModelBindingResult.Success((XElement)container);
            }
            else
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.FieldName,
                    string.Format(SoapMessages.MulitpleElements, second.Name.LocalName));
            }
        }

        private static void BindXmlSerializable(ModelBindingContext bindingContext, XContainer container)
        {
            var modelType = bindingContext.ModelType.IsArray ? bindingContext.ModelType.GetElementType() : bindingContext.ModelType;

            var values = container
                .Elements()
                .Select(element => element.Deserialize(modelType))
                .ToArray();

            if (!bindingContext.ModelType.IsArray)
            {
                if (values.Length > 1)
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.FieldName,
                        string.Format(SoapMessages.MulitpleElements, container.Elements().FirstOrDefault().Name.LocalName));
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(values.FirstOrDefault());
                }
            }
            else
            {
                // We need an ModelType[], not object[] with ModelType in it.
                var array = (Array)Activator.CreateInstance(modelType.MakeArrayType(), values.Length);
                Array.Copy(values, array, values.Length);
                bindingContext.Result = ModelBindingResult.Success(array);
            }
        }
    }
}
