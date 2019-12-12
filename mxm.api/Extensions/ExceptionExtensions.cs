using Microsoft.AspNetCore.Builder;
using mxm.api.Handler;

namespace mxm.api.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ConfigureGlobalException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
        }
    }
}
