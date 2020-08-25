using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using MyWeb.ViewModels;

namespace MyWeb.Errors
{
    public class MyExceptionFilter : IExceptionFilter
    {
        //private readonly IStringLocalizer<Program> _localizer;

        //public MyExceptionFilter(IStringLocalizer<Program> localizer)
        //{
        //    _localizer = localizer;
        //}

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyException ex)
            {
                //var localeMessage = _localizer.GetString(ex.Message);
                var response = Responses.Error(ex.Code, ex.Message);
                context.Result = new OkObjectResult(response);
                context.ExceptionHandled = true;
            }
        }
    }
}
