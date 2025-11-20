using Serilog.Api.Util;
using System.Globalization;


namespace Serilog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {


            try
            {
                var builder = WebApplication.CreateBuilder(args);






                // Logging nativo do .NET
                ConfiguracaoLogger.ConfigurarDotNetLogging(builder);
                ConfiguracaoLogger.Informacao("Configuração de logging iniciada");
                ConfiguracaoLogger.Informacao("Logger Informacao nativo em execução!");
                ConfiguracaoLogger.Erro("Logger Error nativo em execução!");
                ConfiguracaoLogger.Alerta("Logger Alerta nativo em execução!");




                //// Chama o configurador unificado de logging
                //SerilogConfig.ConfigurarSerilog(builder);
                ////builder.Logging.ClearProviders();
                ////builder.Logging.AddConsole();
                ////builder.Logging.AddDebug();


                // Habilitar Logs do EF Core (Queries SQL)

                // builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.Debug);


                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddHostedService<TarefaLog>();
 

                var app = builder.Build();



                ConfiguracaoLogger.RegistrarLoggerViaApp(app);
                ConfiguracaoLogger.Informacao("Logger Informacao nativo em execução!");
                ConfiguracaoLogger.Erro("Logger Error nativo em execução!");
                ConfiguracaoLogger.Alerta("Logger Alerta nativo em execução!");



                //app.UseSerilogRequestLogging(); 
                app.UseRouting();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.UseMiddlewareLog();
                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
                app.Run();
            }
            catch (Exception ex)
            {
                ConfiguracaoLogger.Erro($"A aplicação falhou ao iniciar: {ex.Message}");
            }
            finally
            {
                ConfiguracaoLogger.Erro($"A aplicação falhou");
            }

 
        }
    }
}
