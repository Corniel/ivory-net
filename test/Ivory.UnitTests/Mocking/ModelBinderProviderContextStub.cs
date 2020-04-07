using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Ivory.UnitTests.Mocking
{
    public class ModelBinderProviderContextStub : ModelBinderProviderContext
    {
        public ModelBinderProviderContextStub(ModelMetadata metadata)
        {
            Metadata = metadata;
        }

        public override BindingInfo BindingInfo { get; }

        public override ModelMetadata Metadata { get; }

        public override IModelMetadataProvider MetadataProvider { get; }

        public override IModelBinder CreateBinder(ModelMetadata metadata) => throw new NotSupportedException();
    }
}
