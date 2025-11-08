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


                SerilogConfig.ConfigurarSerilog();
                //  Cria logger manualmente antes do build
                using var loggerFactory = LoggerFactory.Create(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                });

                var logger = loggerFactory.CreateLogger("StartupLogger");

                logger.LogInformation("-----------------------------------------------------------------------------");
                logger.LogInformation("Program.Builder -  Aplicação iniciando... (Logger via LoggerFactory)");
                logger.LogInformation("-----------------------------------------------------------------------------");


                logger.LogInformation("#############################################################################");
                logger.LogInformation("                            TimeZoneInfo                                     ");
                logger.LogInformation("#############################################################################");
                var culture = new CultureInfo("pt-BR");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                // Ajusta timezone (para exibição)
                TimeZoneInfo brZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var horaBrasil = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brZone);
                logger.LogInformation("-----------------------------------------------------------------------------");
                logger.LogInformation($"Hora do Brasil: {horaBrasil}");
                logger.LogInformation("-----------------------------------------------------------------------------");



                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

     
                builder.Services.AddHostedService<TaregaLog>();
                builder.Host.UseSerilog();



                // ?? Remove provedores padrão e adiciona o console (visível no Log Stream)
                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();   // ? Essencial para o Fluxo de Log
                builder.Logging.AddDebug();

                //// ?? Defina nível mínimo (evita filtro silencioso)
                //builder.Logging.SetMinimumLevel(LogLevel.Information);


                var app = builder.Build();

                // 3. Use o logger do Serilog para o pipeline do ASP.NET Core
                app.UseSerilogRequestLogging();

                // O resto da sua configuração...
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

                // ?? Log inicial  
        
                logger.LogInformation("-----------------------------------------------------------------------------");
                logger.LogInformation("Program.App -  Aplicação inicializada com sucesso (Azure Log ativo).");
                logger.LogInformation("-----------------------------------------------------------------------------");


                //  Ping automático (mantém o App “acordado”)
                _ = Task.Run(async () =>
                {
                    var http = new HttpClient();
                    while (true)
                    {
                        try
                        {
                            var response = await http.GetAsync("https://serilog.azurewebsites.net/api/Serilog/log");
                            Log.Information("Ping automático executado. Status: {status}", response.StatusCode);
                        }
                        catch (Exception ex)
                        {
                            Log.Warning(ex, "Falha ao executar ping automático.");
                        }
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
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
