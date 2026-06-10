using Serilog.Api.Util;


namespace Serilog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string environmentName = builder.Environment.EnvironmentName;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<LoggerBackgroundService>(); // serviço de teste de logs
                
                
            var app = builder.Build();
            ConfiguracaoLogger.RegistrarLoggerViaApp(app);
            ConfiguracaoLogger.Informacao("Logger Informacao nativo em execução!");
            ConfiguracaoLogger.Error("Logger Error nativo em execução!");
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
    }
}
