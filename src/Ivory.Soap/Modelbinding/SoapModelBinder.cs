using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

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
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            // This binder is only suited for SOAP requests.
            if (!bindingContext.HttpContext.IsSoapRequest())
            {
                return;
            }

            try
            {
                var soapRequest = await bindingContext.HttpContext.GetSoapRequestAsync();

                var isHeader = bindingContext.FieldName == "header";

                var node = isHeader
                    ? soapRequest.GetSoapHeader()
                    : soapRequest.GetSoapBody();

                var model = node.Deserialize(bindingContext.ModelType);

                if (isHeader || model != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "SOAP body is missing.");
                }
            }
            catch (Exception x)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, x.Message);
            }

            bindingContext.Result = ModelBindingResult.Failed();

        }
    }
}
