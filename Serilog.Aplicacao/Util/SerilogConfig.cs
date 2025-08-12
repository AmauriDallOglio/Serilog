using Serilog.Events;

namespace Serilog.Aplicacao.Util
{
    public static class SerilogConfig
    {
        public static void ConfigurarSerilog()
        {

            // Obtenha a chave do Application Insights  variáveis de ambiente
            string applicationInsightsChave = Environment.GetEnvironmentVariable("APPINSIGHTS_CHAVE") ?? string.Empty;
            if (!string.IsNullOrEmpty(applicationInsightsChave))
            {
                Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose()

                //// Logs Warning
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                //    .WriteTo.File(
                //        path: @"D:\home\LogFiles\warning-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Error
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                //    .WriteTo.File(
                //        path: @"D:\home\LogFiles\error-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Critical
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal)
                //    .WriteTo.File(
                //        path: @"D:\home\LogFiles\critical-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                // Logs Warning e acima (Warning, Error, Fatal) num arquivo agregado
                .WriteTo.File(
                    path: @"D:\home\LogFiles\warning-and-above-.log",
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    rollingInterval: RollingInterval.Day)

                // Log no console (Azure Log Stream)
                .WriteTo.Console()

                // Log para Application Insights (somente erros e acima)
                //No Serilog, quando você usa o parâmetro LogEventLevel.Error no .WriteTo.ApplicationInsights(...), ele significa o nível mínimo de log que será enviado para o Application Insights — ou seja, todos os logs de nível Error e acima (Error, Fatal) serão enviados, mas Warning NÃO será enviado.
                //Resumo da hierarquia de níveis(do mais baixo ao mais alto): Verbose < Debug < Information < Warning < Error < Fatal
                .WriteTo.ApplicationInsights(applicationInsightsChave, TelemetryConverter.Traces, LogEventLevel.Warning)
                .CreateLogger();
            }
            else
            {

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose() // captura todos os níveis


                //// Apenas logs de Trace (nível mais baixo)
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Verbose)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\trace-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Debug
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\debug-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Information
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\information-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))


                //// Logs Warning (somente Warning)
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\warning-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Error (somente Error)
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\error-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                //// Logs Critical (somente Fatal)
                //.WriteTo.Logger(lc => lc
                //    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal)
                //    .WriteTo.File(
                //        path: @"C:\Amauri\GitHub\logs\critical-.log",
                //        rollingInterval: RollingInterval.Day,
                //        retainedFileCountLimit: 30))

                // Logs Warning e superiores em arquivo agregado
                .WriteTo.File(
                    path: @"C:\Amauri\GitHub\logs\warning-and-above-.log",
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    rollingInterval: RollingInterval.Day)

                // Console para todos os logs
                .WriteTo.Console()
                .CreateLogger();

            }
        }


        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCESSO] {message}");
            Console.ResetColor();
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[AVISO] {message}");
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERRO] {message}");
            Console.ResetColor();
        }
    }
}

