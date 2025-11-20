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
            //_logger.LogInformation("#############################################################################");
            //_logger.LogInformation("                            MiddlewareLog                                    ");
            //_logger.LogInformation("#############################################################################");

            //_logger.LogInformation("-----------------------------------------------------------------------------");
            //_logger.LogInformation("MiddlewareLog:  [Middleware foi iniciado]...");
            //_logger.LogInformation("-----------------------------------------------------------------------------");

            //_logger.LogWarning("-----------------------------------------------------------------------------");
            //_logger.LogWarning("MiddlewareLog:  [Middleware foi iniciado em warning]...");
            //_logger.LogWarning("-----------------------------------------------------------------------------");

            //_logger.LogError("-----------------------------------------------------------------------------");
            //_logger.LogError("MiddlewareLog:  [Middleware foi iniciado com erros]...");
            //_logger.LogError("-----------------------------------------------------------------------------");

            //_logger.LogInformation("-----------------------------------------------------------------------------");
            //_logger.LogInformation("MiddlewareLog: Iniciando processamento da requisição {Path}", httpContext.Request.Path);
            //_logger.LogInformation("-----------------------------------------------------------------------------");

            ConfiguracaoLogger.Informacao("****************************************************************************");
            ConfiguracaoLogger.Informacao("                            MiddlewareLog 2                                   ");
            ConfiguracaoLogger.Informacao("****************************************************************************");

            ConfiguracaoLogger.Informacao("-----------------------------------------------------------------------------");
            ConfiguracaoLogger.Informacao("MiddlewareLog:  [Middleware foi iniciado]...");
            ConfiguracaoLogger.Informacao("-----------------------------------------------------------------------------");

            ConfiguracaoLogger.Alerta("-----------------------------------------------------------------------------");
            ConfiguracaoLogger.Alerta("MiddlewareLog:  [Middleware foi iniciado em warning]...");
            ConfiguracaoLogger.Alerta("-----------------------------------------------------------------------------");

            ConfiguracaoLogger.Erro("-----------------------------------------------------------------------------");
            ConfiguracaoLogger.Erro("MiddlewareLog:  [Middleware foi iniciado com erros]...");
            ConfiguracaoLogger.Erro("-----------------------------------------------------------------------------");



            await _next.Invoke(httpContext);

        }
    }
}
