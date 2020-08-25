using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyWeb.utils
{
    public static class ModelStateExtension
    {
        public static string GetFirstErrorMessage(this ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                var value = modelState[key];
                if (value.Errors.Any())
                {
                    return value.Errors.FirstOrDefault().ErrorMessage;
                }
            }
            return "";

            //return modelState.FirstOrDefault(m => m.Value.ValidationState == ModelValidationState.Invalid)
            //    .Value?
            //    .Errors?
            //    .FirstOrDefault()?
            //    .ErrorMessage;
        }



    }
}
