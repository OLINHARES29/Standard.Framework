using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Standard.Framework.Result.Abstractions;
using Standard.Framework.Result.Concretes;
using System.Linq;
using System.Net;

namespace Standard.Framework.Filter.Concretes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                IApplicationResult<object> result = new ApplicationResult<object>() { StatusCode = HttpStatusCode.BadRequest };
                context.ModelState.Keys.ToList().ForEach(it => result.Messages.AddRange(context.ModelState[it].Errors.Select(x => $"{it}: {x.ErrorMessage}")));

                context.Result = result;
            }
        }
    }
}
