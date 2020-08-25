using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWeb.ViewModels;

namespace MyWeb.utils
{
    public class MyValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var response = Responses.ValidationError();
            response.Message = context.ModelState.GetFirstErrorMessage();
            context.Result = new OkObjectResult(response);
        }
    }
}
