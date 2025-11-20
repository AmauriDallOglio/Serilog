namespace Serilog.Api.Util
{
    public class TarefaLog : BackgroundService
    {
        private readonly ILogger<TarefaLog> _logger;
        private int _contador = 0;
        private readonly HttpClient _httpClient;

        public TarefaLog(ILogger<TarefaLog> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            ConfiguracaoLogger.Informacao("                            TarefaLog Iniciada                               ");

            //_logger.LogInformation("#############################################################################");
            //_logger.LogInformation("                            TarefaLog Iniciada                               ");
            //_logger.LogInformation("#############################################################################");

            while (!cancellationToken.IsCancellationRequested)
            {
                _contador++;

                try
                {
                    //_logger.LogInformation("----------------------------------------------------------------------------------");
                    //_logger.LogInformation($"1️  Execução #{_contador}: Log automático disparado");
                    ConfiguracaoLogger.Informacao($"1️  Execução #{_contador}: Log automático disparado");
                    //_logger.LogInformation("----------------------------------------------------------------------------------");

                    //_logger.LogWarning("----------------------------------------------------------------------------------");
                    //_logger.LogWarning($"2️ Aviso de teste - execução #{_contador}");
                    ConfiguracaoLogger.Alerta($"2️ Aviso de teste - execução #{_contador}");
                    //_logger.LogWarning("----------------------------------------------------------------------------------");

                    //_logger.LogError("----------------------------------------------------------------------------------");
                    //_logger.LogError("3️  Exemplo de erro simulado na execução #{contador}", _contador);
                    ConfiguracaoLogger.Erro($"3️  Exemplo de erro simulado na execução #{_contador}");
                    //_logger.LogError("----------------------------------------------------------------------------------");

                    //  Ping automático (mantém o App “acordado”)
                    var response = await _httpClient.GetAsync("https://serilog.azurewebsites.net/api/Serilog/log", cancellationToken);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                       // _logger.LogInformation($"Ping automático executado. Status: {response.StatusCode}");
                        ConfiguracaoLogger.Informacao($"Ping automático executado. Status: {response.StatusCode}");
                    }
                    else
                    {
                       // _logger.LogError($"Ping automático executado. Status: {response.StatusCode}");
                        ConfiguracaoLogger.Erro($"Ping automático executado. Status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                   // _logger.LogError($" Falha durante execução da TarefaLog no ciclo #{_contador}");
                    ConfiguracaoLogger.Alerta($"Falha ao executar ping automático.");
                }

                //  Espera 1 minuto entre cada execução
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }

            ConfiguracaoLogger.Informacao("--> TarefaLog finalizada.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            ConfiguracaoLogger.Informacao(" TarefaLog foi interrompida manualmente.");
            await base.StopAsync(cancellationToken);
        }
    }
}
