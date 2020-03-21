﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace Ivory.Soap.UnitTests.Mocking
{
    public class ModelBindingContextStub : ModelBindingContext
    {
        public override ActionContext ActionContext { get; set; }
        public override string BinderModelName { get; set; }
        public override BindingSource BindingSource { get; set; }
        public override string FieldName { get; set; }
        public override bool IsTopLevelObject { get; set; } = true;
        public override object Model { get; set; }
        public override ModelMetadata ModelMetadata { get; set; }
        public override string ModelName { get; set; }
        public override ModelStateDictionary ModelState { get; set; } = new ModelStateDictionary();
        public override Func<ModelMetadata, bool> PropertyFilter { get; set; }
        public override ModelBindingResult Result { get; set; }
        public override ValidationStateDictionary ValidationState { get; set; }
        public override IValueProvider ValueProvider { get; set; }

        public override NestedScope EnterNestedScope() =>throw new NotSupportedException();
        public override NestedScope EnterNestedScope(ModelMetadata modelMetadata, string fieldName, string modelName, object model) => throw new NotSupportedException();
        protected override void ExitNestedScope() => throw new NotSupportedException();
    }
}
