using EloBaza.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EloBaza.WebApi.Middleware
{
    class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next,
            IActionResultExecutor<ObjectResult> executor,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _executor = executor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
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

                var result = CreateResult(ex, httpContext);
                await _executor.ExecuteAsync(actionContext, result);
                return;
            }
        }

        private ObjectResult CreateResult(Exception ex, HttpContext httpContext)
        {
            ProblemDetails problemDetails;
            if (ex is ValidationException)
            {
                problemDetails = new ValidationProblemDetails((ex as ValidationException)?.Errors)
                {
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Instance = httpContext.Request.Path,
                    Title = ex.GetType().Name
                };

                return new BadRequestObjectResult(problemDetails);
            }
            else if (ex is NotFoundException)
            {
                problemDetails = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Instance = httpContext.Request.Path,
                    Title = ex.GetType().Name
                };

                return new NotFoundObjectResult(problemDetails);
            }
            else if (ex is AlreadyExistsException)
            {
                problemDetails = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = StatusCodes.Status409Conflict,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    Instance = httpContext.Request.Path,
                    Title = ex.GetType().Name
                };

                return new ConflictObjectResult(problemDetails);
            }
            else
            {
                throw ex;
            }
        }
    }
}
