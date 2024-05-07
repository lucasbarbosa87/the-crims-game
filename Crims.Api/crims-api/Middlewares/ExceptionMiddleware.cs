using Crims.Core.Failures;
using Crims.Data.Dtos;
using Newtonsoft.Json;
using System.Net;

namespace crims_api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Failure ex)
            {
                await HandleExceptionAsync(context, ex, ex.StatusCode);
                string message = $"Have error type of {ex.GetType}: {ex.Message}";
                _logger.LogError(ex, message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
                _logger.LogError(ex, ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            if (statusCode == HttpStatusCode.BadRequest)
            {
                await context.Response.WriteAsync(exception.Message);
                return;
            }
            var response = new ApiErrorDto(exception.Message, "00");
            var jsonResponse = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
