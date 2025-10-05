using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabourChowk_webapi.Middleware
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalException> _logger;
        public GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new { message = "An unexpected error occurred. Please try again later." };
                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}