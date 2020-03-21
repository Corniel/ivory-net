using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Ivory.Soap.Modelbinding
{
    /// <summary>Implements an <see cref="IModelBinderProvider"/> for SOAP.</summary>
    public class SoapModelBinderProvider : IModelBinderProvider
    {
        private readonly SoapModelBinder[] binders = new SoapModelBinder[]
        {
            new SoapBodyModelBinder(),
            new SoapHeaderModelBinder(),
            new SoapEnvelopeModelBinder(),
        };

        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Guard.NotNull(context, nameof(context));
            return binders.FirstOrDefault(b => b.CanBind(context.Metadata));
        }
    }
}
