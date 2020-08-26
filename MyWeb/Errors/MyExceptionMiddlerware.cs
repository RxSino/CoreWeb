using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWeb.utils;
using MyWeb.ViewModels;

namespace MyWeb.Errors
{
    public class MyExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public MyExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(httpContext, ex);
            }
        }

        private async Task WriteExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            httpContext.Response.ContentType = "application/json";

            var response = Responses.Error();

            if (exception != null)
            {
                if (exception is MyException ex)
                {
                    response = new RawResponse
                    {
                        Code = ex.Code,
                        Message = ex.Value,
                        Data = ex.ToString()
                    };
                }
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, response, JsonUtils.DefaultOptions());
            }
        }

    }
}
