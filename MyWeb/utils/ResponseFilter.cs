using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWeb.utils
{
    public class ResponseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
