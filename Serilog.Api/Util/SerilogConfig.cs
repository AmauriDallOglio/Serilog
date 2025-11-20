namespace Serilog.Api.Util
{
    public static class SerilogConfig
    {
 

        //public static void ConfigurarSerilog(WebApplicationBuilder builder)
        //{


        //    // =====================================================================
        //    // 🔹 1. Limpa provedores padrão e ativa console/debug para o pipeline
        //    // =====================================================================
        //    builder.Logging.ClearProviders();
        //    builder.Logging.AddConsole(); // visível no Azure Log Stream
        //    builder.Logging.AddDebug();   // útil localmente

        //    // =====================================================================
        //    // 🔹 2. Define o ambiente (Azure x Local)
        //    // =====================================================================
        //    string azure = Environment.GetEnvironmentVariable("LOG_AZURE") ?? string.Empty;

        //    if (!string.IsNullOrEmpty(azure))
        //    {
        //        // Ambiente Azure (Log Stream ativo)
        //        Log.Logger = new LoggerConfiguration()
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

        //        Log.Information("--------------------------------------------------------------------------------");
        //        Log.Information("✅ Amauri Versão 1.9.3 ");
        //        Log.Information("Ambiente: AZURE LOG STREAM ativo (LOG_AZURE detectado).");
        //        Log.Information("--------------------------------------------------------------------------------");
        //    }
        //    else
        //    {
        //        // Ambiente local (desenvolvimento)
        //        Log.Logger = new LoggerConfiguration()
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

        //        Log.Information("--------------------------------------------------------------------------------");
        //        Log.Information("Ambiente local detectado (sem LOG_AZURE).");
        //        Log.Information("--------------------------------------------------------------------------------");
        //    }

        //    // =====================================================================
        //    // 🔹 3. Configura o host para usar Serilog
        //    // =====================================================================
        //    builder.Host.UseSerilog();

        //    // =====================================================================
        //    // 🔹 4. Ajuste de cultura e fuso horário
        //    // =====================================================================
        //    try
        //    {
        //        var culture = new CultureInfo("pt-BR");
        //        CultureInfo.DefaultThreadCurrentCulture = culture;
        //        CultureInfo.DefaultThreadCurrentUICulture = culture;

        //        TimeZoneInfo brZone;
        //        try
        //        {
        //            brZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // Windows
        //        }
        //        catch (TimeZoneNotFoundException)
        //        {
        //            brZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // Linux
        //        }

        //        var horaBrasil = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brZone);
        //        Log.Information("-----------------------------------------------------------------------------");
        //        Log.Information("                            TimeZoneInfo                                     ");
        //        Log.Information($" Hora do Brasil: {horaBrasil}");
        //        Log.Information($" Timezone: {brZone.DisplayName}");
        //        Log.Information("-----------------------------------------------------------------------------");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, " Falha ao configurar cultura e timezone pt-BR.");
        //    }
        //}


        //public static void Info(string message)
        //{
        //    Log.Information("----------------------------------------------------");
        //    Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
        //    Log.Information("----------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    Console.WriteLine($"[INFO] {message}");
        //    Console.ResetColor();
        //}

        //public static void Success(string message)
        //{
        //    Log.Information("----------------------------------------------------");
        //    Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
        //    Log.Information("----------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine($"[SUCESSO] {message}");
        //    Console.ResetColor();
        //}

        //public static void Warning(string message)
        //{
        //    Log.Information("----------------------------------------------------");
        //    Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
        //    Log.Information("----------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine($"[AVISO] {message}");
        //    Console.ResetColor();
        //}

        //public static void Error(string message)
        //{
        //    Log.Information("----------------------------------------------------");
        //    Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
        //    Log.Information("----------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine($"[ERRO] {message}");
        //    Console.ResetColor();
        //}
    }
}





 