using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using System.Linq;

namespace Ivory.Soap.UnitTests.TestTools
{
    public static class ModelBindAssert
    {
        public static TModel Success<TModel>(ModelBindingResult result)
            where TModel : class
        {
            var model = result.Model as TModel;
            Assert.NotNull(model);
            return model;
        }
        public static TModel[] SuccessArray<TModel>(ModelBindingResult result)
           where TModel : class
        {
            var models = (result.Model as object[])?.Cast<TModel>().ToArray();
            Assert.NotNull(models);
            return models;
        }
    }
}
