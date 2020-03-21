using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Implementation of an <see cref="IModelBinder"/> for SOAP.</summary>
    public class SoapModelBinder : IModelBinder, IModelBinderProvider
    {
        private const string envelope = nameof(envelope);
        private const string header = nameof(header);
        private const string body = nameof(body);

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
            if (!bindingContext.HttpContext.Request.IsSoapRequest())
            {
                return;
            }

            var isHeader = bindingContext.FieldName == header;

            if (!isHeader && bindingContext.FieldName != body)
            {
                return;
            }

            var message = await GetSoapMessageAsync(bindingContext);

            if (message is null)
            {
                return;
            }

            var content = isHeader ? message.Header : message.Body;

            object model;

            try
            {
                model = ((XElement)content).Deserialize(bindingContext.ModelType);
            }
            catch (Exception x)
            {
                x = x.InnerException is null ? x : x.InnerException;
                bindingContext.ModelState.AddModelError(bindingContext.FieldName, x.Message);
                return;
            }

            if (!isHeader && model is null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.FieldName, "SOAP body is missing.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }
            bindingContext.Result = ModelBindingResult.Success(model);
        }

        private static async Task<SoapMessage> GetSoapMessageAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelState.TryGetValue(envelope, out var entry))
            {
                return (SoapMessage)entry.RawValue;
            }

            var stream = bindingContext.HttpContext.Request.Body;
            try
            {
                var message = await SoapMessage.LoadAsync(stream);
                bindingContext.ModelState.SetModelValue(envelope, message, string.Empty);
                return message;
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return null;
        }
    }
}
