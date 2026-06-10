using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog.Aplicacao.Dto;

namespace Serilog.Api.Util
{
    public static class ConfiguracaoLogger
    {
 
        private static ILogger? _loggerTemp;

        public static void ConfigurarDotNetLogging(WebApplicationBuilder builder, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettingsDto>();

            if (appSettings is null)
            {
                Console.WriteLine("[ConfiguracaoLogger] AVISO: AppSettingsDto não pôde ser lido. Logging padrão mantido.");
                return;
            }

            builder.Logging.ClearProviders();

            if (!appSettings.ConfiguracoesLog.Ativado)
            {
                Console.WriteLine("[ConfiguracaoLogger] Logging DESATIVADO via appsettings.");
                return;
            }

            // Provedores
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddAzureWebAppDiagnostics();
            builder.Services.AddApplicationInsightsTelemetry();

            // Níveis
            LogLevel nivelGlobal = MapearNivel(appSettings.ConfiguracoesLog.Nivel);
            builder.Logging.SetMinimumLevel(nivelGlobal);

            LogLevel nivelAzure = appSettings.ConfiguracoesLog.NivelAzure is not null
                ? MapearNivel(appSettings.ConfiguracoesLog.NivelAzure)
                : nivelGlobal;

            // Configurações do Azure
            builder.Services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "app-log-";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 5;
            });

            builder.Services.Configure<AzureBlobLoggerOptions>(options =>
            {
                options.BlobName = "app-log.txt";
            });

            // Filtros
            ConfigurarFiltros(builder, nivelGlobal);

            // Logger temporário
            _loggerTemp = CriarLoggerTemporario(nivelGlobal);
            _loggerTemp.LogInformation(
                "------ Logging configurado | Ativado: {Ativado} | Nível Global: {Nivel} ({LogLevel}) | Nível Azure: {NivelAzure} ({LogLevelAzure})",
                appSettings.ConfiguracoesLog.Ativado,
                appSettings.ConfiguracoesLog.Nivel,
                nivelGlobal,
                appSettings.ConfiguracoesLog.NivelAzure ?? appSettings.ConfiguracoesLog.Nivel,
                nivelAzure);
        }

        private static void ConfigurarFiltros(WebApplicationBuilder builder, LogLevel nivelGlobal)
        {
            builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
            builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            builder.Logging.AddFilter("System", LogLevel.Warning);

            LogLevel nivelEfCore = nivelGlobal == LogLevel.Trace ? LogLevel.Debug : LogLevel.None;
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", nivelEfCore);
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", nivelEfCore);
        }

        private static ILogger CriarLoggerTemporario(LogLevel nivelGlobal) =>
            LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(nivelGlobal);
            }).CreateLogger("ConfiguracaoLogger");

        private static LogLevel MapearNivel(string nivel) => nivel switch
        {
            "Detalhado" => LogLevel.Trace,
            "Informacoes" => LogLevel.Information,
            "Aviso" => LogLevel.Warning,
            "Erro" => LogLevel.Error,
            _ => LogLevel.Information
        };

 
         
         
         

        //// ================================================================
        ////  CONFIGURAÇÃO DO SERILOG
        //// ================================================================
        //public static void ConfigurarSerilog(WebApplicationBuilder builder)
        //{


        //    // =====================================================================
        //    // 1. Limpa provedores padrão e ativa console/debug para o pipeline
        //    // =====================================================================
        //    builder.Logging.ClearProviders();
        //    builder.Logging.AddConsole(); // visível no Azure Log Stream
        //    builder.Logging.AddDebug();   // útil localmente

        //    // =====================================================================
        //    // 2. Define o ambiente (Azure x Local)
        //    // =====================================================================
        //    string azure = Environment.GetEnvironmentVariable("LOG_AZURE") ?? string.Empty;

        //    if (!string.IsNullOrEmpty(azure))
        //    {
        //        // Ambiente Azure (Log Stream ativo)
        //        Serilog.Log.Logger = new LoggerConfiguration()
        //            .MinimumLevel.Information()
        //            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        //            .WriteTo.Console(
        //                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        //            )
        //            .WriteTo.File(
        //                path: @"D:\home\LogFiles\application_log-.txt",
        //                rollingInterval: RollingInterval.Day,
        //                restrictedToMinimumLevel: LogEventLevel.Information,
        //                shared: true
        //            )
        //            .CreateLogger();

        //        Serilog.Log.Information("--------------------------------------------------------------------------------");
        //        Serilog.Log.Information("✅ Amauri Versão 1.9.3 ");
        //        Serilog.Log.Information("Ambiente: AZURE LOG STREAM ativo (LOG_AZURE detectado).");
        //        Serilog.Log.Information("--------------------------------------------------------------------------------");
        //    }
        //    else
        //    {
        //        // Ambiente local (desenvolvimento)
        //        Serilog.Log.Logger = new LoggerConfiguration()
        //            .MinimumLevel.Information()
        //            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        //            .WriteTo.Console(
        //                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        //            )
        //            .WriteTo.File(
        //                path: @"C:\Amauri\GitHub\logs\app-.log",
        //                rollingInterval: RollingInterval.Day,
        //                shared: true
        //            )
        //            .CreateLogger();

        //        Serilog.Log.Information("--------------------------------------------------------------------------------");
        //        Serilog.Log.Information("Ambiente local detectado (sem LOG_AZURE).");
        //        Serilog.Log.Information("--------------------------------------------------------------------------------");
        //    }

        //    // =====================================================================
        //    // 3. Configura o host para usar Serilog
        //    // =====================================================================
        //    builder.Host.UseSerilog();

        //}


        // ================================================================
        // 2. Abre um canal para registrar o ILogger na classe
        // ================================================================

 
 

 

    }
}
