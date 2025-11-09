using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
            _logger.LogInformation("#############################################################################");
            _logger.LogInformation("                            TarefaLog Iniciada                               ");
            _logger.LogInformation("#############################################################################");

            while (!cancellationToken.IsCancellationRequested)
            {
                _contador++;

                try
                {
                    _logger.LogInformation("----------------------------------------------------------------------------------");
                    _logger.LogInformation("1️  Execução #{contador}: Log automático disparado", _contador);
                    Log.Information("1️  Execução #{contador}: Log automático disparado", _contador);
                    _logger.LogInformation("----------------------------------------------------------------------------------");

                    _logger.LogWarning("----------------------------------------------------------------------------------");
                    _logger.LogWarning("2️ Aviso de teste - execução #{contador}", _contador);
                    Log.Warning("2️ Aviso de teste - execução #{contador}", _contador);
                    _logger.LogWarning("----------------------------------------------------------------------------------");

                    _logger.LogError("----------------------------------------------------------------------------------");
                    _logger.LogError("3️  Exemplo de erro simulado na execução #{contador}", _contador);
                    Log.Error("3️  Exemplo de erro simulado na execução #{contador}", _contador);
                    _logger.LogError("----------------------------------------------------------------------------------");

                    //  Ping automático (mantém o App “acordado”)
                    var response = await _httpClient.GetAsync("https://serilog.azurewebsites.net/api/Serilog/log", cancellationToken);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogInformation("Ping automático executado. Status: {status}", response.StatusCode);
                        Log.Information("Ping automático executado. Status: {status}", response.StatusCode);
                    }
                    else
                    {
                        _logger.LogError("Ping automático executado. Status: {status}", response.StatusCode);
                        Log.Error("Ping automático executado. Status: {status}", response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, " Falha durante execução da TarefaLog no ciclo #{contador}", _contador);
                    Log.Warning(ex, "Falha ao executar ping automático.");
                }

                //  Espera 1 minuto entre cada execução
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }

            _logger.LogInformation("--> TarefaLog finalizada.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(" TarefaLog foi interrompida manualmente.");
            await base.StopAsync(cancellationToken);
        }
    }
}
