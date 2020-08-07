using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Behaviors
{
    class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Handling {requestName}", typeof(TRequest).Name);
            _logger.LogDebug("Request: {@request}", request);

            var response = await next();

            stopwatch.Stop();
            _logger.LogInformation("Handled {requestName}. Elapsed time: {elapsedMilliseconds} ms.", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);
            _logger.LogDebug("Response: {@response}", response);

            return response;
        }
    }
}
