using System.Net;
using System.Text.Json;

namespace AuthApex.WebApi.Middleware
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            switch (exception)
            {
                case ArgumentException:
                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;

                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound;   // 404
                    break;

                default:
                    status = HttpStatusCode.InternalServerError; // 500
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var response = JsonSerializer.Serialize(new
            {
                error = message,
                statusCode = context.Response.StatusCode
            });

            return context.Response.WriteAsync(response);
        }
    }
}
