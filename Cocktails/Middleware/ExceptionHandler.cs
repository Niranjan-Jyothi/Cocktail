using System.ComponentModel.DataAnnotations;
using System.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cocktails.Services.Services;
using Cocktails.Services.Models;
using System.Globalization;

namespace Cocktails.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (ValidationException ex)
            {
                LogMessage(httpContext,"Validation Exception", ex);
                await SendResponse(httpContext, 400, new { ex.Message });
            }
            catch (SecurityException ex)
            {
                LogMessage(httpContext, "Forbidden", ex);
                await SendResponse(httpContext, 403, new { ex.Message });
            }
            catch (Exception ex)
            {
                LogMessage(httpContext, "Unexpected error", ex);
                await SendResponse(httpContext, 500, new { Message = "Unexpected error." });
            }
        }

        //Function to send custom response back to client.
        private static async Task SendResponse(HttpContext context, int statusCode, object response)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        //Function to log to DB.
        private static void LogMessage(HttpContext context, string logMessage, Exception ex)
        {
            var logService = context.RequestServices.GetRequiredService<ILogService>();
            

            context.Items.TryGetValue(Constants.InDataKey, out var indData);
            context.Items.TryGetValue(Constants.MethodNameKey, out var methodName);
            context.Items.TryGetValue(Constants.RequestTimeKey, out var requestDate);

            logService.AddLog(new CocktailLog
            {
                OutData = ex.Message,
                ReplyDateTime = DateTime.UtcNow,
                InData = indData?.ToString() ?? string.Empty,
                MethodName = methodName?.ToString() ?? string.Empty,
                RequestDateTime = DateTime.Parse(requestDate?.ToString() ?? DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                ErrorMessage = logMessage
            });
        }
    }
}
