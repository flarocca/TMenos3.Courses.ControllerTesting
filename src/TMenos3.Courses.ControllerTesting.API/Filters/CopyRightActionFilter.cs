using Microsoft.AspNetCore.Mvc.Filters;
using TMenos3.Courses.ControllerTesting.API.Infrastructure;

namespace TMenos3.Courses.ControllerTesting.API.Filters
{
    public class CopyRightActionFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Response.Headers.ContainsKey(Constants.CopyrightHeader) == false)
                context.HttpContext.Response.Headers.Add(Constants.CopyrightHeader, Constants.CopyrightValue);
        }
    }
}
