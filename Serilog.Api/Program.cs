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

                // Chama o configurador unificado de logging
                SerilogConfig.ConfigurarSerilog(builder);
                //builder.Logging.ClearProviders();
                //builder.Logging.AddConsole();
                //builder.Logging.AddDebug();

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddHostedService<TarefaLog>();
 

                var app = builder.Build();


                app.UseSerilogRequestLogging(); 
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
                Log.Fatal(ex, "A aplicação falhou ao iniciar");
            }
            finally
            {
                Log.CloseAndFlush();
            }

 
        }
    }
}
