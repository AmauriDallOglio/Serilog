namespace Serilog.Api.Util
{
    public class LoggerBackgroundService : BackgroundService
    {
        private readonly ILogger<LoggerBackgroundService> _logger;
        private readonly string _nivel;

        public LoggerBackgroundService(
            ILogger<LoggerBackgroundService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _nivel = configuration.GetSection("ConfiguracoesLog")["Nivel"] ?? "Informacoes";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("------ LoggerBackgroundService iniciado. Nível configurado: {Nivel}", _nivel);

            while (!stoppingToken.IsCancellationRequested)
            {
                EmitirLogs();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("------ LoggerBackgroundService encerrado.");
        }

        /// <summary>
        /// Emite os logs de acordo com o nível configurado.
        /// Cada case demonstra exatamente quais mensagens devem aparecer
        /// para aquele nível no Azure App Service Logs.
        /// </summary>
        private void EmitirLogs()
        {
            switch (_nivel)
            {
                // Detalhado → Trace, Debug, Information, Warning, Error
                case "Detalhado":
                    _logger.LogTrace("------ [Detalhado]   LogTrace       — nível mais granular");
                    _logger.LogDebug("------ [Detalhado]   LogDebug       — diagnóstico detalhado");
                    _logger.LogInformation("------ [Detalhado]   LogInformation — fluxo normal");
                    _logger.LogWarning("------ [Detalhado]   LogWarning     — situação inesperada");
                    _logger.LogError("------ [Detalhado]   LogError       — falha recuperável");
                    break;

                // Informacoes → Information, Warning, Error
                case "Informacoes":
                    _logger.LogInformation("------ [Informacoes] LogInformation — fluxo normal");
                    _logger.LogWarning("------ [Informacoes] LogWarning     — situação inesperada");
                    _logger.LogError("------ [Informacoes] LogError       — falha recuperável");
                    break;

                // Aviso → Warning, Error
                case "Aviso":
                    _logger.LogWarning("------ [Aviso] LogWarning — situação inesperada");
                    _logger.LogError("------ [Aviso] LogError   — falha recuperável");
                    break;

                // Erro → apenas Error
                case "Erro":
                    _logger.LogError("------ [Erro] LogError — falha recuperável");
                    break;

                // Fallback para qualquer valor não mapeado
                default:
                    _logger.LogInformation(
                        "------ [Padrão] Nível '{Nivel}' não reconhecido. Emitindo LogInformation.", _nivel);
                    break;
            }
        }

    }
}
