using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Model binder for the SOAP envelope as <see cref="XDocument"/>.</summary>
    public class SoapBodyModelBinder : SoapModelBinder
    {
        /// <inheritdoc/>
        public override BindingSource BindingSource => SoapBindingSource.SoapBody;

        /// <inheritdoc/>
        protected override async Task<XContainer> GetContainerAysnc(ModelBindingContext bindingContext)
        {
            var envelope = await GetEnvelopeAsync(bindingContext);
            return envelope?.Root.Element(envelope.Root.Name.Namespace + "Body");
        }
    }
}
