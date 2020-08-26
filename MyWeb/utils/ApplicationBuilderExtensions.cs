using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MyWeb.Errors;
using MyWeb.ViewModels;

namespace MyWeb.utils
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder MyExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = InternalExceptionHandler,
            });
        }

        public static async Task InternalExceptionHandler(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";

            var response = Responses.Error();
            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

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

                //var stream = httpContext.Response.Body;
                //await JsonSerializer.SerializeAsync(stream, response, JsonSerializerOptions());

                var text = JsonSerializer.Serialize(response, JsonUtils.DefaultOptions());
                await context.Response.WriteAsync(text);
            }
        }


    }
}
