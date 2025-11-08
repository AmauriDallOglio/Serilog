namespace Serilog.Api.Util
{
    public class MiddlewareLog
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareLog> _logger; 

        public MiddlewareLog(RequestDelegate next, ILogger<MiddlewareLog> logger)
        {
            _next = next;
            _logger = logger;


        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("#############################################################################");
            _logger.LogInformation("                            MiddlewareLog                                    ");
            _logger.LogInformation("#############################################################################");

            _logger.LogInformation("-----------------------------------------------------------------------------");
            _logger.LogInformation("MiddlewareLog:  [Middleware foi iniciado]...");
            _logger.LogInformation("-----------------------------------------------------------------------------");

            _logger.LogWarning("-----------------------------------------------------------------------------");
            _logger.LogWarning("MiddlewareLog:  [Middleware foi iniciado em warning]...");
            _logger.LogWarning("-----------------------------------------------------------------------------");

            _logger.LogError("-----------------------------------------------------------------------------");
            _logger.LogError("MiddlewareLog:  [Middleware foi iniciado com erros]...");
            _logger.LogError("-----------------------------------------------------------------------------");

            _logger.LogInformation("-----------------------------------------------------------------------------");
            _logger.LogInformation("MiddlewareLog: Iniciando processamento da requisição {Path}", httpContext.Request.Path);
            _logger.LogInformation("-----------------------------------------------------------------------------");

            await _next.Invoke(httpContext);

        }
    }
}
