namespace Serilog.Api.Util
{
    public class MiddlewareLog
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareLog> _logger;
        private readonly string _nivel;

        public MiddlewareLog(RequestDelegate next, ILogger<MiddlewareLog> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _nivel = configuration.GetSection("ConfiguracoesLog")["Nivel"] ?? "Informacoes";
        }

        public async Task Invoke(HttpContext httpContext)
        {

            _logger.LogInformation("****************************************************************************");
            _logger.LogInformation("                            MiddlewareLog 2                                   ");
            _logger.LogInformation("****************************************************************************");

            _logger.LogInformation("-----------------------------------------------------------------------------");
            _logger.LogInformation("MiddlewareLog:  [LogInformation]...");
            _logger.LogInformation("-----------------------------------------------------------------------------");

            _logger.LogWarning("-----------------------------------------------------------------------------");
            _logger.LogWarning("MiddlewareLog:  [LogWarningg]...");
            _logger.LogWarning("-----------------------------------------------------------------------------");

            _logger.LogError("-----------------------------------------------------------------------------");
            _logger.LogError("MiddlewareLog:  [LogError]...");
            _logger.LogError("-----------------------------------------------------------------------------");

            _logger.LogTrace("-----------------------------------------------------------------------------");
            _logger.LogTrace("MiddlewareLog:  [LogTrace]...");
            _logger.LogTrace("-----------------------------------------------------------------------------");

            _logger.LogDebug("-----------------------------------------------------------------------------");
            _logger.LogDebug("MiddlewareLog:  [LogDebug]...");
            _logger.LogDebug("-----------------------------------------------------------------------------");





            switch (_nivel)
            {
                // Detalhado → Trace, Debug, Information, Warning, Error
                case "Detalhado":
                    _logger.LogInformation("****************************************************************************");
                    _logger.LogInformation("                            MiddlewareLog 2                                   ");
                    _logger.LogInformation("****************************************************************************");

                    _logger.LogInformation("-----------------------------------------------------------------------------");
                    _logger.LogInformation("MiddlewareLog:  [LogInformation]...");
                    _logger.LogInformation("-----------------------------------------------------------------------------");

                    _logger.LogWarning("-----------------------------------------------------------------------------");
                    _logger.LogWarning("MiddlewareLog:  [LogWarningg]...");
                    _logger.LogWarning("-----------------------------------------------------------------------------");

                    _logger.LogError("-----------------------------------------------------------------------------");
                    _logger.LogError("MiddlewareLog:  [LogError]...");
                    _logger.LogError("-----------------------------------------------------------------------------");

                    _logger.LogTrace("-----------------------------------------------------------------------------");
                    _logger.LogTrace("MiddlewareLog:  [LogTrace]...");
                    _logger.LogTrace("-----------------------------------------------------------------------------");

                    _logger.LogDebug("-----------------------------------------------------------------------------");
                    _logger.LogDebug("MiddlewareLog:  [LogDebug]...");
                    _logger.LogDebug("-----------------------------------------------------------------------------");


                    break;

                // Informacoes → Information, Warning, Error
                case "Informacoes":
                    _logger.LogInformation("****************************************************************************");
                    _logger.LogInformation("                            MiddlewareLog 2                                   ");
                    _logger.LogInformation("****************************************************************************");

                    _logger.LogInformation("-----------------------------------------------------------------------------");
                    _logger.LogInformation("MiddlewareLog:  [LogInformation]...");
                    _logger.LogInformation("-----------------------------------------------------------------------------");

                    _logger.LogWarning("-----------------------------------------------------------------------------");
                    _logger.LogWarning("MiddlewareLog:  [LogWarning]...");
                    _logger.LogWarning("-----------------------------------------------------------------------------");

                    _logger.LogError("-----------------------------------------------------------------------------");
                    _logger.LogError("MiddlewareLog:  [LogError]...");
                    _logger.LogError("-----------------------------------------------------------------------------");

                    break;

                // Aviso → Warning, Error
                case "Aviso":
 
                    _logger.LogWarning("-----------------------------------------------------------------------------");
                    _logger.LogWarning("MiddlewareLog:  [LogWarning]...");
                    _logger.LogWarning("-----------------------------------------------------------------------------");

                    _logger.LogError("-----------------------------------------------------------------------------");
                    _logger.LogError("MiddlewareLog:  [LogError]...");
                    _logger.LogError("-----------------------------------------------------------------------------");

                    break;

                // Erro → apenas Error
                case "Erro":
 
                    _logger.LogError("-----------------------------------------------------------------------------");
                    _logger.LogError("MiddlewareLog:  [LogError]...");
                    _logger.LogError("-----------------------------------------------------------------------------");

                    break;

                // Fallback para qualquer valor não mapeado
                default:
                    _logger.LogInformation("------ [Padrão] Nível '{Nivel}' não reconhecido. Emitindo LogInformation.", _nivel);
                    break;
            }






            await _next.Invoke(httpContext);

        }
    }
}
