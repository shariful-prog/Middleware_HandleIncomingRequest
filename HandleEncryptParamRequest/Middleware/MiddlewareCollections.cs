using Microsoft.AspNetCore.Builder;

namespace HandleEncryptParamRequest.Middleware
{
    public static class MiddlewareCollections
    {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpRequestMiddleware>();
        }
    }
}
