using Newtonsoft.Json;
using System.Security.Authentication;

namespace TaxiServiceAPI.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="next">The next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                if (context == null)
                {
                    _logger.LogInformation("Request Method context is Null");
                    return;
                }

                _logger.LogInformation($"Request Method using IMiddleware: {context.Request.Method}, Path: {context.Request.Path}");
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            _logger.LogError(exception, "An error occurred while processing the request");
            context.Response.StatusCode = statusCode;

            Object errorResponse = new
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace
            };

            string jsonResponse = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
