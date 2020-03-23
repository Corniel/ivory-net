using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Model binder for the SOAP envelope as <see cref="XDocument"/>.</summary>
    public class SoapEnvelopeModelBinder : SoapModelBinder
    {
        /// <inheritdoc/>
        public override BindingSource BindingSource => SoapBindingSource.SoapEnvelope;

        /// <inheritdoc/>
        public override async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.NotNull(bindingContext, nameof(bindingContext));

            var envelope = await GetEnvelopeAsync(bindingContext);

            bindingContext.Result = bindingContext.ModelType == typeof(XDocument)
                ? ModelBindingResult.Success(envelope)
                : ModelBindingResult.Success(envelope.Root.Deserialize(bindingContext.ModelType));
        }

        /// <inheritdoc>/>
        protected override async Task<XContainer> GetContainerAysnc(ModelBindingContext bindingContext)
        {
            return await GetEnvelopeAsync(bindingContext);
        }
    }
}
