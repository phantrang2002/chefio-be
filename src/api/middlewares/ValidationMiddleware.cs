using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Chefio.Api.Middlewares
{
    public class JsonExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JsonExceptionMiddleware> _logger;

        public JsonExceptionMiddleware(RequestDelegate next, ILogger<JsonExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parse error");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var field = ex.Path?.Replace("$.", "") ?? "json";

                var errorResponse = new
                {
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = new Dictionary<string, string>
                    {
                        [field] = $"{field} has invalid format or type."
                    }
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                if (ex is BadHttpRequestException badRequestEx)
                {
                    _logger.LogError(badRequestEx, "Bad request error");

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        title = "One or more validation errors occurred.",
                        status = 400,
                        errors = new Dictionary<string, string>
                        {
                            ["request"] = "Invalid data format or type."
                        }
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}