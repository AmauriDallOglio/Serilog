using Serilog.Events;


namespace Serilog.Api.Util
{
    public static class SerilogConfig
    {
 
 
        public static void ConfigurarSerilog()
        {

 
            // Obtenha a chave do Application Insights  variáveis de ambiente
            string azure = Environment.GetEnvironmentVariable("LOG_AZURE") ?? string.Empty;
            if (!string.IsNullOrEmpty(azure))
            {

                // Bootstrap logger para capturar logs iniciais
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Information()
                      .MinimumLevel.Verbose() // captura todos os níveis
                      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Ignora logs de infra
                      .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                      .MinimumLevel.Override("System", LogEventLevel.Warning)

                    .WriteTo.Console()
                    .WriteTo.File(
                        path: @"D:\home\LogFiles\application_startup.log",
                        rollingInterval: RollingInterval.Day)
                    .CreateBootstrapLogger();



                //var loggerConfiguration = new LoggerConfiguration()
                //   .MinimumLevel.Verbose() // Captura todos os níveis de log
                //   .Enrich.FromLogContext();

                //// --- CONFIGURAÇÃO PARA O AZURE ---
                //Log.Information("Detectado ambiente Azure ({site}). Aplicando configuração de produção.", azure);

                //// Escreve no Console/Trace para aparecer no Log Stream do Azure
                //loggerConfiguration.WriteTo.Trace(LogEventLevel.Verbose);

                //// Escreve em um arquivo persistente no Azure
                //loggerConfiguration.WriteTo.File(
                //    path: @"D:\home\LogFiles\log-de-producao-.txt",
                //    rollingInterval: RollingInterval.Day,
                //    restrictedToMinimumLevel: LogEventLevel.Information
                //);

                ////Log.Logger = new LoggerConfiguration()
                ////    .MinimumLevel.Verbose()
                ////    // Para ver no Log Stream
                ////    .WriteTo.Console()
                ////    // Para salvar em arquivos persistentes no Azure
                ////    .WriteTo.File(
                ////        path: @"D:\home\LogFiles\warning-and-above-.log",
                ////        restrictedToMinimumLevel: LogEventLevel.Warning,
                ////        rollingInterval: RollingInterval.Day)
                ////    .CreateLogger();
                Log.Information("----------------------------------------------------");
                Log.Information("Program.ConfigurarSerilog: Log de teste no Azure Log Stream!");
                Log.Information("----------------------------------------------------");
            }
            else
            {


                Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Information()
                  .MinimumLevel.Verbose() // captura todos os níveis
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Ignora logs de infra
                  .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                  .MinimumLevel.Override("System", LogEventLevel.Warning)

                  .WriteTo.Console(
                      outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                  )
                  .WriteTo.File(
                      path: @"D:\home\LogFiles\application-log-.txt",
                      rollingInterval: RollingInterval.Day,
                      restrictedToMinimumLevel: LogEventLevel.Information,
                      shared: true
                  )
                  .CreateLogger();

                Log.Information("-----------------------------------------------------------------");
                Log.Information("Program.ConfigurarSerilog: Log Stream, não possui configuração no azure!");
                Log.Information("-----------------------------------------------------------------");


                ////Log.Logger = new LoggerConfiguration()
                ////    .MinimumLevel.Verbose() // captura todos os níveis
                //// Configuração do logger
                //Log.Logger = new LoggerConfiguration()
                //    .MinimumLevel.Verbose() // Captura todos os níveis
                //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Ignora logs de infra
                //    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                //    .MinimumLevel.Override("System", LogEventLevel.Warning)

                //    //// Apenas logs de Trace (nível mais baixo)
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Verbose)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\trace-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))

                //    //// Logs Debug
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\debug-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))

                //    //// Logs Information
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\information-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))


                //    //// Logs Warning (somente Warning)
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\warning-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))

                //    //// Logs Error (somente Error)
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\error-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))

                //    //// Logs Critical (somente Fatal)
                //    //.WriteTo.Logger(lc => lc
                //    //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal)
                //    //    .WriteTo.File(
                //    //        path: @"C:\Amauri\GitHub\logs\critical-.log",
                //    //        rollingInterval: RollingInterval.Day,
                //    //        retainedFileCountLimit: 30))



                //    // Logs Warning e superiores em arquivo
                //    .WriteTo.File(
                //    path: @"C:\Amauri\GitHub\logs\warning-and-above-.log",
                //    restrictedToMinimumLevel: LogEventLevel.Warning,
                //    rollingInterval: RollingInterval.Day)
                //// Todos os logs no console
                //.WriteTo.Console()
                //.CreateLogger();

                //Log.Information("----------------------------------------------------");
                //Log.Information("ConfigurarSerilog: Log Stream, não possui configuração no azure!");
                //Log.Information("----------------------------------------------------");

            }
        }


        public static void Info(string message)
        {
            Log.Information("----------------------------------------------------");
            Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
            Log.Information("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }

        public static void Success(string message)
        {
            Log.Information("----------------------------------------------------");
            Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
            Log.Information("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCESSO] {message}");
            Console.ResetColor();
        }

        public static void Warning(string message)
        {
            Log.Information("----------------------------------------------------");
            Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
            Log.Information("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[AVISO] {message}");
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Log.Information("----------------------------------------------------");
            Log.Information("ConfigurarSerilog: Log de teste no Azure Log Stream!");
            Log.Information("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERRO] {message}");
            Console.ResetColor();
        }
    }
}





 