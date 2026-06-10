using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog.Aplicacao.Dto;

namespace Serilog.Api.Util
{
    public static class ConfiguracaoLogger
    {
 
        private static ILogger? _loggerTemp;

        public static void ConfigurarDotNetLogging(WebApplicationBuilder builder, IConfiguration configuration)
        {
            AppSettingsDto? appSettings = configuration.Get<AppSettingsDto>();

            if (appSettings is null)
            {
                Console.WriteLine("[ConfiguracaoLogger] AVISO: AppSettingsDto não pôde ser lido. Logging padrão mantido.");
                return;
            }

            // Remove todos os provedores padrão (Console, Debug, EventLog, etc.)
            builder.Logging.ClearProviders();

            if (!appSettings.ConfiguracoesLog.Ativado)
            {
                Console.WriteLine("[ConfiguracaoLogger] Logging DESATIVADO via appsettings.");
                return;
            }

            // ---------------------------------------------------------------
            // Provedores
            // ---------------------------------------------------------------
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddAzureWebAppDiagnostics();        // Azure App Service Logs (filesystem + blob)
            builder.Services.AddApplicationInsightsTelemetry(); // Application Insights

            // ---------------------------------------------------------------
            // Nível global — aplicado ao Console e Debug
            // ---------------------------------------------------------------
            LogLevel nivelGlobal = MapearNivel(appSettings.ConfiguracoesLog.Nivel);
            builder.Logging.SetMinimumLevel(nivelGlobal);

            // ---------------------------------------------------------------
            // Nível exclusivo para o Fluxo de log do Azure App Service
            // Se NivelAzure não for informado, herda o mesmo nível global
            // ---------------------------------------------------------------
            LogLevel nivelAzure = appSettings.ConfiguracoesLog.NivelAzure is not null
                ? MapearNivel(appSettings.ConfiguracoesLog.NivelAzure)
                : nivelGlobal;



            // Adiciona provider de logs do Azure App Service
            builder.Logging.AddAzureWebAppDiagnostics();



            // Configurações do arquivo de log no filesystem do Azure
            builder.Services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "app-log-"; // prefixo do arquivo gerado
                options.FileSizeLimit = 50 * 1024;  // 50 KB por arquivo
                options.RetainedFileCountLimit = 5;          // mantém os 5 arquivos mais recentes
            });

            // Configurações do blob de log no Azure Storage (opcional)
            builder.Services.Configure<AzureBlobLoggerOptions>(options =>
            {
                options.BlobName = "app-log.txt";
            });

            // ---------------------------------------------------------------
            // Filtros por categoria — evita flood de logs internos do .NET/ASP.NET
            // ---------------------------------------------------------------
            builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
            builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            builder.Logging.AddFilter("System", LogLevel.Warning);

            // EF Core: exibe queries SQL apenas no nível Detalhado (Trace)
            LogLevel nivelEfCore = nivelGlobal == LogLevel.Trace ? LogLevel.Debug : LogLevel.None;
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", nivelEfCore);
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", nivelEfCore);

            // ---------------------------------------------------------------
            // Logger temporário para mensagens ANTES do app.Build()
            // (o ILogger do DI ainda não está disponível neste momento)
            // ---------------------------------------------------------------
            ILogger loggerTemp = LoggerFactory
                .Create(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(nivelGlobal);
                })
                .CreateLogger("ConfiguracaoLogger");

            loggerTemp.LogInformation(
                "------ Logging configurado | Ativado: {Ativado} | Nível Global: {Nivel} ({LogLevel}) | Nível Azure: {NivelAzure} ({LogLevelAzure})",
                appSettings.ConfiguracoesLog.Ativado,
                appSettings.ConfiguracoesLog.Nivel,
                nivelGlobal,
                appSettings.ConfiguracoesLog.NivelAzure ?? appSettings.ConfiguracoesLog.Nivel,
                nivelAzure);
        }




        // -----------------------------------------------------------------------
        // Mapeamento de string → LogLevel (espelha o portal do Azure)
        // -----------------------------------------------------------------------
        private static LogLevel MapearNivel(string nivel) => nivel switch
        {
            "Detalhado" => LogLevel.Trace,       // Trace + Debug + Info + Warning + Error
            "Informacoes" => LogLevel.Information,  // Info + Warning + Error
            "Aviso" => LogLevel.Warning,      // Warning + Error
            "Erro" => LogLevel.Error,        // Apenas Error
            _ => LogLevel.Information   // Padrão seguro
        };



        // ================================================================
        //  CONFIGURAÇÃO DO LOG NATIVO .NET
        // ================================================================
        public static void ConfigurarDotNetLogging(WebApplicationBuilder builder)
        {
            // ----------------------------------------------
            //  CONFIGURAÇÃO DE LOG DO BUILDER
            // ----------------------------------------------
            // Remove provedores padrão
            builder.Logging.ClearProviders();

            // Console local
            builder.Logging.AddConsole();

            // Debug (somente ambiente local)
            builder.Logging.AddDebug();

            // Azure Diagnostics (envia para Log Stream)
            //Instalar Microsoft.Extensions.Logging.AzureAppServices
            builder.Logging.AddAzureWebAppDiagnostics();

            // Application Insights (telemetria + logs + métricas)
            //Instalar Microsoft.ApplicationInsights.AspNetCore
            builder.Services.AddApplicationInsightsTelemetry();

            //  Configurar níveis de log por categoria
            builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
            builder.Logging.AddFilter("System", LogLevel.Warning);

            // Configurar seu projeto inteiro
            builder.Logging.AddFilter("Serialog", LogLevel.Information);


            //  Ou nível global
            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);
            // builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.Debug);

            // ----------------------------------------------
            // CRIAR LOGGER TEMPORÁRIO MANUAL
            // (funciona ANTES do app.Build())
            // ----------------------------------------------


            _loggerTemp = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            }).CreateLogger("PipelineBuilder");

            _loggerTemp.LogWarning(" ***** Amauri Versão 1.0 ***** ");
            _loggerTemp.LogInformation(" Logging nativo configurado para o builder.");
            _loggerTemp.LogWarning(" Pipeline do builder iniciado.");
            _loggerTemp.LogInformation(" Azure Log Diagnostics configurado.");

        }

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

        public static void RegistrarLoggerViaApp(WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            RegistrarLogger(logger);

            _loggerTemp?.LogInformation("-------------------------------------");
            Informacao("Aplicação iniciada!");
            _loggerTemp?.LogInformation(" Migrando do logger temporário para o logger final.");
            _loggerTemp?.LogInformation("-------------------------------------");

        }

        private static void RegistrarLogger(ILogger logger)
        {
            _loggerTemp = logger;
        }



        // =====================================================================
        // 3. MÉTODOS DE LOG (unifica Serilog + ILogger + Console Color)
        // =====================================================================

        public static void Informacao(string mensagem)
        {
            //Log.Information(mensagem);
            (_loggerTemp ?? _loggerTemp)?.LogInformation(mensagem);
            Colorir(mensagem, ConsoleColor.Black, ConsoleColor.Green);
        }

        public static void Sucesso(string mensagem)
        {
            var mensagemTmp = $"SUCESSO: {mensagem}";
            //Log.Information(mensagemTmp);
            (_loggerTemp ?? _loggerTemp)?.LogInformation(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.Black, ConsoleColor.Cyan);
        }

        public static void Error(string mensagem)
        {
            var mensagemTmp = $"Erro: {mensagem}";
            // Log.Error(mensagemTmp);
            (_loggerTemp)?.LogError(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.White, ConsoleColor.Red);
        }

        public static void Alerta(string mensagem)
        {
            var mensagemTmp = $"Alerta: {mensagem}";
            // Log.Warning(mensagemTmp);
            (_loggerTemp)?.LogWarning(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        public static void Detalhado(string mensagem)
        {
            var mensagemTmp = $"Detalhado: {mensagem}";
            //Log.Debug(mensagemTmp);
            (_loggerTemp)?.LogDebug(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.DarkGray, ConsoleColor.Black);
        }

        private static void Colorir(string mensagem, ConsoleColor fg, ConsoleColor bg)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mensagem}");
            Console.ResetColor();
        }

    }
}
