using Core;
using Dto;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Runtime;

namespace API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptioAsync(ex, httpContext);
            }
        }

        private async Task HandleExceptioAsync(Exception ex, HttpContext context)
        {
            int status = 500;
            if (ex is APIException)
            {
                status = (ex as APIException).StatusCode;
            }
            
            context.Response.StatusCode = status;

            var res = new BaseResponse(status, ex, ex.Message);

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
            };

            await context.Response.WriteAsJsonAsync(JsonConvert.SerializeObject(res, settings));

        }
    }
}
