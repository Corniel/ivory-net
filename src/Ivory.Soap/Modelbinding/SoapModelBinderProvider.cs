using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Ivory.Soap.Modelbinding
{
    public class SoapModelBinderProvider : IModelBinderProvider
    {
        private readonly SoapModelBinder[] binders = new SoapModelBinder[] 
        {
            new SoapBodyModelBinder(),
            new SoapHeaderModelBinder(),
            new SoapEvenlopeModelBinder(),
        };

        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Guard.NotNull(context, nameof(context));
            return binders.FirstOrDefault(b => b.CanBind(context.Metadata));
        }
    }
}
