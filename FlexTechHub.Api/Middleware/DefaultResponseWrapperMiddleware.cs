using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Api.Middleware
{
    public class DefaultResponse<T>
    {
        public bool Status { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public T Data { get; set; }
    }

    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            if (ShouldExclude(context.Request.Path))
            {
                await _next(context);
                return;
            }

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                    return; // Ensure we don't continue processing after handling the exception
                }
                finally
                {
                    context.Response.Body = originalBodyStream;
                    responseBody.Seek(0, SeekOrigin.Begin);

                    var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                    responseBody.Seek(0, SeekOrigin.Begin);

                    // Always return a wrapped response
                    var wrappedResponse = new DefaultResponse<object>
                    {
                        Status = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        ResponseMessage = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300
                            ? "Success"
                            : "Error",
                        ResponseCode = context.Response.StatusCode,
                        Data = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300
                            ? JsonSerializer.Deserialize<object>(responseBodyText)
                            : null,
                    };

                    var jsonResponse = JsonSerializer.Serialize(wrappedResponse, new JsonSerializerOptions { WriteIndented = true });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }

        private bool ShouldExclude(PathString path)
        {
            return path.StartsWithSegments("/swagger") ||
                   path.StartsWithSegments("/swagger-ui") ||
                   path.StartsWithSegments("/favicon.ico");
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new DefaultResponse<object>
            {
                Status = false,
                ResponseMessage = "Internal Server Error: " + ex.Message,
                ResponseCode = StatusCodes.Status500InternalServerError,
                Data = null,
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
