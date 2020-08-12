using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EloBaza.MailService.Middleware
{
    class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _executor = executor;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var routeData = httpContext.GetRouteData();
                var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

                var canBeHandeld = TryCreateResult(ex, httpContext, out var result);
                if (!canBeHandeld)
                    throw;

                await _executor.ExecuteAsync(actionContext, result!);
            }
        }

        private static bool TryCreateResult(Exception ex, HttpContext httpContext, out ObjectResult? result)
        {
            ProblemDetails problemDetails;
            if (ex is ArgumentException || ex is ArgumentNullException)
            {
                problemDetails = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/400",
                    Instance = httpContext.Request.Path,
                    Title = ex.GetType().Name
                };

                result = new BadRequestObjectResult(problemDetails);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}
