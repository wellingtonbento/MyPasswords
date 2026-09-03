using Microsoft.AspNetCore.Diagnostics;
using MyPasswords.Exceptions;

namespace MyPasswords.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exceção não tratada em {Method} {Path}",
            httpContext.Request.Method, httpContext.Request.Path);

            var redirectUrl = exception switch
            {
                EmailAlreadyExistsException =>
                    $"/Account/Register?error={Uri.EscapeDataString(exception.Message)}",

                InvalidCredentialsException =>
                    $"/Account/Login?error={Uri.EscapeDataString(exception.Message)}",

                NotFoundException =>
                    $"/Home/Error?message={Uri.EscapeDataString(exception.Message)}",

                _ => "/Home/Error"
            };

            httpContext.Response.Redirect(redirectUrl);
            return ValueTask.FromResult(true);
        }
    }
}
