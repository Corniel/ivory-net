using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Model binder for the SOAP envelope as <see cref="XDocument"/>.</summary>
    public class SoapEvenlopeModelBinder : SoapModelBinder
    {
        /// <inheritdoc/>
        public override bool CanBind(ModelMetadata metadata)
        {
            return metadata != null
                && metadata.ParameterName == "envelope"
                && BindingSourceIsBody(metadata);
        }

        /// <inheritdoc/>
        public override async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            var envelope = await GetEnvelopeAsync(bindingContext);

            if (bindingContext.ModelType == typeof(XDocument))
            {
                bindingContext.Result = ModelBindingResult.Success(envelope);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(envelope.Root.Deserialize(bindingContext.ModelType));
            }
        }

        /// <inheritdoc>/>
        protected override async Task<XContainer> GetContainerAysnc(ModelBindingContext bindingContext)
        {
            return await GetEnvelopeAsync(bindingContext);
        }
    }
}
