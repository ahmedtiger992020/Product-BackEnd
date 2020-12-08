using Microsoft.AspNetCore.Builder;

namespace Sample.Web
{
    public static class MiddlewareRegister
    {
        public static void UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
