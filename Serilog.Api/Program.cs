using Serilog.Aplicacao.Util;
using Serilog.Events;

namespace Serilog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

 

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configura o Serilog
           SerilogConfig.ConfigurarSerilog();
            builder.Host.UseSerilog();
  

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            // Redirecionar a raiz "/" para "/swagger"
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });


            app.UseHsts();
            app.UseStaticFiles();
            app.UseRouting();
 

            app.Run();
        }
    }
}
