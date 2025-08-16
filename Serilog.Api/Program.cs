using Serilog.Aplicacao.Util;
using Serilog.Events;
using Serilog;
using Serilog.Api;  


namespace Serilog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {


            try
            {
                var builder = WebApplication.CreateBuilder(args);
 

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();


                SerilogConfig.ConfigurarSerilog();
                builder.Host.UseSerilog();



                var app = builder.Build();

                // 3. Use o logger do Serilog para o pipeline do ASP.NET Core
                app.UseSerilogRequestLogging();

                // O resto da sua configuração...
                app.UseRouting();

                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

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


            // var builder = WebApplication.CreateBuilder(args);




            // builder.Services.AddControllers();

            // builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();


            // // Configura o Serilog
            //SerilogConfig.ConfigurarSerilog();
            // builder.Host.UseSerilog();


            // var app = builder.Build();
            // app.UseSwagger();
            // app.UseSwaggerUI();
            // app.UseHttpsRedirection();
            // app.UseAuthorization();
            // app.MapControllers();


            // // Redirecionar a raiz "/" para "/swagger"
            // app.MapGet("/", context =>
            // {
            //     context.Response.Redirect("/swagger");
            //     return Task.CompletedTask;
            // });


            // app.UseHsts();
            // app.UseStaticFiles();
            // app.UseRouting();


            // app.Run();
        }
    }
}
