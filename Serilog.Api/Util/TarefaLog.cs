namespace Serilog.Api.Util
{
    public class TaregaLog : BackgroundService
    {
        private readonly ILogger<TaregaLog> _logger;
        private int _contador = 0;

        public TaregaLog(ILogger<TaregaLog> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("#############################################################################");
            _logger.LogInformation("                            TaregaLog                                        ");
            _logger.LogInformation("#############################################################################");

            while (!cancellationToken.IsCancellationRequested)
            {
                _contador++;

                _logger.LogInformation("----------------------------------------------------------------------------------");
                _logger.LogInformation("1 - TaregaLog - Log automático disparado — execução #{contador}", _contador);
                _logger.LogInformation("----------------------------------------------------------------------------------");

                _logger.LogInformation("----------------------------------------------------------------------------------");
                _logger.LogWarning("2 - TaregaLog - Este é um log de teste (Warning)");
                _logger.LogInformation("----------------------------------------------------------------------------------");

                _logger.LogInformation("----------------------------------------------------------------------------------");
                _logger.LogError("3 - TaregaLog - Exemplo de erro simulado no ciclo #{contador}", _contador);
                _logger.LogInformation("--------------------------------------------------------");


                // Aguarda 1 minuto
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }

            _logger.LogInformation("--> TaregaLog finalizado.");
        }
    }
}
