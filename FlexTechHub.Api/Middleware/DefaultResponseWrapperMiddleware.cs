// using System;
// using System.Diagnostics;
// using System.IO;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;

// namespace InventoryManagement.Api.Middleware
// {
//     public class DefaultResponse<T>
//     {
//         public bool Status { get; set; }
//         public string ResponseMessage { get; set; }
//         public int ResponseCode { get; set; }
//         public T Data { get; set; }
//     }

//     public class ResponseWrapperMiddleware
//     {
//         private readonly RequestDelegate _next;

//         public ResponseWrapperMiddleware(RequestDelegate next)
//         {
//             _next = next;
//         }

//         public async Task InvokeAsync(HttpContext context)
//         {
//             var originalBodyStream = context.Response.Body;

//             using (var responseBody = new MemoryStream())
//             {
//                 context.Response.Body = responseBody;

//                 try
//                 {
//                     await _next(context);
//                 }
//                 catch (Exception ex)
//                 {
//                     await HandleExceptionAsync(context, ex);
//                     return; // Ensure that we don't continue processing after handling the exception
//                 }

//                 // Read the response body
//                 context.Response.Body = originalBodyStream;
//                 responseBody.Seek(0, SeekOrigin.Begin);
//                 var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
//                 responseBody.Seek(0, SeekOrigin.Begin);

//                 // Determine if the response was successful
//                 var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

//                 // Create the response object
//                 var response = new DefaultResponse<object>
//                 {
//                     Status = isSuccess,
//                     ResponseMessage =!isSuccess && context.Response.StatusCode == StatusCodes.Status500InternalServerError ? responseBodyText :  GetReasonPhrase(context.Response.StatusCode),
//                     ResponseCode = context.Response.StatusCode,
//                     Data = isSuccess ? JsonSerializer.Deserialize<object>(responseBodyText) : null,
//                 };

//                 var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
//                 context.Response.ContentType = "application/json";
//                 await context.Response.WriteAsync(jsonResponse);
//             }
//         }

//         private Task HandleExceptionAsync(HttpContext context, Exception ex)
//         {
//             context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//             var response = new DefaultResponse<object>
//             {
//                 Status = false,
//                 ResponseMessage = "Internal Server Error : "+ ex.Message,
//                 ResponseCode = StatusCodes.Status500InternalServerError,
//                 Data = null,
//             };

//             var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
//             context.Response.ContentType = "application/json";
//             return context.Response.WriteAsync(jsonResponse);
//         }

//         private string GetReasonPhrase(int statusCode)
//         {
//             return statusCode switch
//             {
//                 StatusCodes.Status200OK => "OK",
//                 StatusCodes.Status201Created => "Created",
//                 StatusCodes.Status202Accepted => "Accepted",
//                 StatusCodes.Status204NoContent => "No Content",
//                 StatusCodes.Status400BadRequest => "Bad Request",
//                 StatusCodes.Status401Unauthorized => "Unauthorized",
//                 StatusCodes.Status403Forbidden => "Forbidden",
//                 StatusCodes.Status404NotFound => "Not Found",
//                 StatusCodes.Status405MethodNotAllowed => "Method Not Allowed",
//                 StatusCodes.Status406NotAcceptable => "Not Acceptable",
//                 StatusCodes.Status407ProxyAuthenticationRequired => "Proxy Authentication Required",
//                 StatusCodes.Status408RequestTimeout => "Request Timeout",
//                 StatusCodes.Status409Conflict => "Conflict",
//                 StatusCodes.Status410Gone => "Gone",
//                 StatusCodes.Status411LengthRequired => "Length Required",
//                 StatusCodes.Status412PreconditionFailed => "Precondition Failed",
//                 StatusCodes.Status413PayloadTooLarge => "Payload Too Large",
//                 StatusCodes.Status414UriTooLong => "URI Too Long",
//                 StatusCodes.Status415UnsupportedMediaType => "Unsupported Media Type",
//                 StatusCodes.Status416RangeNotSatisfiable => "Range Not Satisfiable",
//                 StatusCodes.Status417ExpectationFailed => "Expectation Failed",
//                 StatusCodes.Status426UpgradeRequired => "Upgrade Required",
//                 StatusCodes.Status428PreconditionRequired => "Precondition Required",
//                 StatusCodes.Status429TooManyRequests => "Too Many Requests",
//                 StatusCodes.Status431RequestHeaderFieldsTooLarge => "Request Header Fields Too Large",
//                 StatusCodes.Status451UnavailableForLegalReasons => "Unavailable For Legal Reasons",
//                 StatusCodes.Status500InternalServerError => "Internal Server Error",
//                 StatusCodes.Status501NotImplemented => "Not Implemented",
//                 StatusCodes.Status502BadGateway => "Bad Gateway",
//                 StatusCodes.Status503ServiceUnavailable => "Service Unavailable",
//                 StatusCodes.Status504GatewayTimeout => "Gateway Timeout",
//                 _ => "Unknown Status"
//             };
//         }
//     }
// }









// using System;
// using System.Diagnostics;
// using System.IO;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;

// namespace InventoryManagement.Api.Middleware
// {
//     public class DefaultResponse<T>
//     {
//         public bool Status { get; set; }
//         public string ResponseMessage { get; set; }
//         public int ResponseCode { get; set; }
//         public T Data { get; set; }
//     }

//     public class ResponseWrapperMiddleware
//     {
//         private readonly RequestDelegate _next;

//         public ResponseWrapperMiddleware(RequestDelegate next)
//         {
//             _next = next;
//         }

//         public async Task InvokeAsync(HttpContext context)
//         {
//             var originalBodyStream = context.Response.Body;

//             // Exclude Swagger and other specific paths
//             if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/swagger-ui") || context.Request.Path.StartsWithSegments("/swagger-ui/index.html") || context.Request.Path.StartsWithSegments("/favicon.ico"))
//             {
//                 await _next(context);
//                 return;
//             }

//             using (var responseBody = new MemoryStream())
//             {
//                 context.Response.Body = responseBody;

//                 try
//                 {
//                     await _next(context);
//                 }
//                 catch (Exception ex)
//                 {
//                     await HandleExceptionAsync(context, ex);
//                     return; // Ensure that we don't continue processing after handling the exception
//                 }

//                 context.Response.Body = originalBodyStream;
//                 responseBody.Seek(0, SeekOrigin.Begin);
//                 var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
//                 responseBody.Seek(0, SeekOrigin.Begin);

//                 if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
//                 {
//                     var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

//                     object data = null;
//                     try
//                     {
//                         data = JsonSerializer.Deserialize<object>(responseBodyText);
//                     }
//                     catch (JsonException)
//                     {
//                         // Handle the case where deserialization fails
//                     }

//                     var response = new DefaultResponse<object>
//                     {
//                         Status = isSuccess,
//                         ResponseMessage = !isSuccess && context.Response.StatusCode == StatusCodes.Status500InternalServerError ? responseBodyText : GetReasonPhrase(context.Response.StatusCode),
//                         ResponseCode = context.Response.StatusCode,
//                         Data = isSuccess ? data : null,
//                     };

//                     var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
//                     context.Response.ContentType = "application/json";
//                     await context.Response.WriteAsync(jsonResponse);
//                 }
//                 else
//                 {
//                     await responseBody.CopyToAsync(originalBodyStream);
//                 }
//             }
//         }

//         private Task HandleExceptionAsync(HttpContext context, Exception ex)
//         {
//             context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//             var response = new DefaultResponse<object>
//             {
//                 Status = false,
//                 ResponseMessage = "Internal Server Error: " + ex.Message,
//                 ResponseCode = StatusCodes.Status500InternalServerError,
//                 Data = null,
//             };

//             var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
//             context.Response.ContentType = "application/json";
//             return context.Response.WriteAsync(jsonResponse);
//         }

//         private string GetReasonPhrase(int statusCode)
//         {
//             return statusCode switch
//             {
//                 StatusCodes.Status200OK => "OK",
//                 StatusCodes.Status201Created => "Created",
//                 StatusCodes.Status202Accepted => "Accepted",
//                 StatusCodes.Status204NoContent => "No Content",
//                 StatusCodes.Status400BadRequest => "Bad Request",
//                 StatusCodes.Status401Unauthorized => "Unauthorized",
//                 StatusCodes.Status403Forbidden => "Forbidden",
//                 StatusCodes.Status404NotFound => "Not Found",
//                 StatusCodes.Status405MethodNotAllowed => "Method Not Allowed",
//                 StatusCodes.Status406NotAcceptable => "Not Acceptable",
//                 StatusCodes.Status407ProxyAuthenticationRequired => "Proxy Authentication Required",
//                 StatusCodes.Status408RequestTimeout => "Request Timeout",
//                 StatusCodes.Status409Conflict => "Conflict",
//                 StatusCodes.Status410Gone => "Gone",
//                 StatusCodes.Status411LengthRequired => "Length Required",
//                 StatusCodes.Status412PreconditionFailed => "Precondition Failed",
//                 StatusCodes.Status413PayloadTooLarge => "Payload Too Large",
//                 StatusCodes.Status414UriTooLong => "URI Too Long",
//                 StatusCodes.Status415UnsupportedMediaType => "Unsupported Media Type",
//                 StatusCodes.Status416RangeNotSatisfiable => "Range Not Satisfiable",
//                 StatusCodes.Status417ExpectationFailed => "Expectation Failed",
//                 StatusCodes.Status426UpgradeRequired => "Upgrade Required",
//                 StatusCodes.Status428PreconditionRequired => "Precondition Required",
//                 StatusCodes.Status429TooManyRequests => "Too Many Requests",
//                 StatusCodes.Status431RequestHeaderFieldsTooLarge => "Request Header Fields Too Large",
//                 StatusCodes.Status451UnavailableForLegalReasons => "Unavailable For Legal Reasons",
//                 StatusCodes.Status500InternalServerError => "Internal Server Error",
//                 StatusCodes.Status501NotImplemented => "Not Implemented",
//                 StatusCodes.Status502BadGateway => "Bad Gateway",
//                 StatusCodes.Status503ServiceUnavailable => "Service Unavailable",
//                 StatusCodes.Status504GatewayTimeout => "Gateway Timeout",
//                 _ => "Unknown Status"
//             };
//         }
//     }
// }


















































using System;
using System.Diagnostics;
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

            // Exclude Swagger and other specific paths
            if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/swagger-ui") || context.Request.Path.StartsWithSegments("/favicon.ico"))
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
                    return; // Ensure that we don't continue processing after handling the exception
                }

                context.Response.Body = originalBodyStream;
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                {
                    var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

                    object data = null;
                    try
                    {
                        data = JsonSerializer.Deserialize<object>(responseBodyText);
                    }
                       catch (JsonException)
                    {
                        // Handle the case where deserialization fails
                    }
                      catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                    return; // Ensure that we don't continue processing after handling the exception
                }
                 

                    var response = new DefaultResponse<object>
                    {
                        Status = isSuccess,
                        ResponseMessage = !isSuccess && context.Response.StatusCode == StatusCodes.Status500InternalServerError ? responseBodyText : GetReasonPhrase(context.Response.StatusCode),
                        ResponseCode = context.Response.StatusCode,
                        Data = isSuccess ? data : null,
                    };

                    var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);
                }
                else
                {
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string responseMessage;

            switch (ex)
            {
                case UnauthorizedAccessException _:
                    statusCode = StatusCodes.Status401Unauthorized;
                    responseMessage = ex.Message;
                    break;
                case NullReferenceException _:
                    statusCode = StatusCodes.Status404NotFound;
                    responseMessage = ex.Message;
                    break;
                // Add other specific exception cases here
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    responseMessage = "Internal Server Error: " + ex.Message;
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new DefaultResponse<object>
            {
                Status = false,
                ResponseMessage = responseMessage,
                ResponseCode = statusCode,
                Data = null,
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonResponse);
        }

        private string GetReasonPhrase(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status200OK => "OK",
                StatusCodes.Status201Created => "Created",
                StatusCodes.Status202Accepted => "Accepted",
                StatusCodes.Status204NoContent => "No Content",
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status405MethodNotAllowed => "Method Not Allowed",
                StatusCodes.Status406NotAcceptable => "Not Acceptable",
                StatusCodes.Status407ProxyAuthenticationRequired => "Proxy Authentication Required",
                StatusCodes.Status408RequestTimeout => "Request Timeout",
                StatusCodes.Status409Conflict => "Conflict",
                StatusCodes.Status410Gone => "Gone",
                StatusCodes.Status411LengthRequired => "Length Required",
                StatusCodes.Status412PreconditionFailed => "Precondition Failed",
                StatusCodes.Status413PayloadTooLarge => "Payload Too Large",
                StatusCodes.Status414UriTooLong => "URI Too Long",
                StatusCodes.Status415UnsupportedMediaType => "Unsupported Media Type",
                StatusCodes.Status416RangeNotSatisfiable => "Range Not Satisfiable",
                StatusCodes.Status417ExpectationFailed => "Expectation Failed",
                StatusCodes.Status426UpgradeRequired => "Upgrade Required",
                StatusCodes.Status428PreconditionRequired => "Precondition Required",
                StatusCodes.Status429TooManyRequests => "Too Many Requests",
                StatusCodes.Status431RequestHeaderFieldsTooLarge => "Request Header Fields Too Large",
                StatusCodes.Status451UnavailableForLegalReasons => "Unavailable For Legal Reasons",
                StatusCodes.Status500InternalServerError => "Internal Server Error",
                StatusCodes.Status501NotImplemented => "Not Implemented",
                StatusCodes.Status502BadGateway => "Bad Gateway",
                StatusCodes.Status503ServiceUnavailable => "Service Unavailable",
                StatusCodes.Status504GatewayTimeout => "Gateway Timeout",
                _ => "Unknown Status"
            };
        }
    }
}
