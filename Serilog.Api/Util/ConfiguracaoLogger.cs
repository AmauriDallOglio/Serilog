namespace Serilog.Api.Util
{
    public static class ConfiguracaoLogger
    {
        private static ILogger? _loggerDotNet;
        private static ILogger? _loggerTemp;

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

            _loggerDotNet?.LogInformation("-------------------------------------");
            Informacao("Aplicação iniciada!");
            _loggerTemp?.LogInformation(" Migrando do logger temporário para o logger final.");
            _loggerDotNet?.LogInformation("-------------------------------------");

        }

        private static void RegistrarLogger(ILogger logger)
        {
            _loggerDotNet = logger;
        }



        // =====================================================================
        // 3. MÉTODOS DE LOG (unifica Serilog + ILogger + Console Color)
        // =====================================================================

        public static void Informacao(string mensagem)
        {
            //Log.Information(mensagem);
            (_loggerDotNet ?? _loggerTemp)?.LogInformation(mensagem);
            Colorir(mensagem, ConsoleColor.Black, ConsoleColor.Green);
        }

        public static void Sucesso(string mensagem)
        {
            var mensagemTmp = $"SUCESSO: {mensagem}";
            //Log.Information(mensagemTmp);
            (_loggerDotNet ?? _loggerTemp)?.LogInformation(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.Black, ConsoleColor.Cyan);
        }

        public static void Erro(string mensagem)
        {
            var mensagemTmp = $"Erro: {mensagem}";
            // Log.Error(mensagemTmp);
            (_loggerDotNet ?? _loggerTemp)?.LogError(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.White, ConsoleColor.Red);
        }

        public static void Alerta(string mensagem)
        {
            var mensagemTmp = $"Alerta: {mensagem}";
            // Log.Warning(mensagemTmp);
            (_loggerDotNet ?? _loggerTemp)?.LogWarning(mensagemTmp);
            Colorir(mensagemTmp, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        public static void Detalhado(string mensagem)
        {
            var mensagemTmp = $"Detalhado: {mensagem}";
            //Log.Debug(mensagemTmp);
            (_loggerDotNet ?? _loggerTemp)?.LogDebug(mensagemTmp);
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
