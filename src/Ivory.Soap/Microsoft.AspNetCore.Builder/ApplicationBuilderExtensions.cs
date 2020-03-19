using Ivory;
using Ivory.Soap;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIvorySoap(this IApplicationBuilder builder)
        {
            return Guard.NotNull(builder, nameof(builder))
                .UseMiddleware<IvorySoapMiddleware>();
        }
    }
}
