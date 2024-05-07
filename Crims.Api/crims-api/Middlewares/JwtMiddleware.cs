using Crims.Domain.Services;

namespace Crims.Authentication.Middlewares
{
    public class JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<JwtMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context, IJwtService jwtHelper)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            var result = await jwtHelper.ValidateJwt(token ?? "");
            Console.WriteLine("Verificacao");

            if (result != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = result;
            }
            await _next(context);
        }
    }
}
