using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateModelStateFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("ValidateModelStateFilter: OnActionExecuting called");

        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key.Replace("$.", ""), // Tên trường
                    kvp => kvp.Value.Errors.First().ErrorMessage // Lấy lỗi đầu tiên cho mỗi trường
                );

            var errorResponse = new
            {
                status = "Error",
                message = "One or more validation errors occurred.",
                errors = errors
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}