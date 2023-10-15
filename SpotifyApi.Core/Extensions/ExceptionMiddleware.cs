using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SpotifyApi.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            string message = ex.Message;
            if (ex.GetType() == typeof(ValidationException))
            {
                message = ex.Message;
            }


            ErrorResultValidation err = new()
            {
                MessageCode = "unknown_err",
                Message = message,
                Data = null
            };
            var json = JsonConvert.SerializeObject(err);
            return httpContext.Response.WriteAsync(json);
        }

        public class ErrorResultValidation
        {
            public List<object> Data { get; set; }
            public string Message { get; set; }
            public string MessageCode { get; set; }

        }
    }
}
