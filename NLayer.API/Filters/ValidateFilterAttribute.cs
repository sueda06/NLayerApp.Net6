using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using PointoFrameworks.StatusCodes.ClientError;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
          if(!context.ModelState.IsValid) 
            {
                var errors =context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(BadRequest.BadRequestResponse());
            }
        }
    }
}
