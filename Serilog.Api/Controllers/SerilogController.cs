using Microsoft.AspNetCore.Mvc;
using Serilog.Aplicacao.Dto;

namespace Serilog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class SerilogController : ControllerBase
    {


        private readonly ILogger<SerilogController> _logger;


        public SerilogController(ILogger<SerilogController> logger)
        {
            _logger = logger;
            _logger.LogInformation("#############################################################################");
            _logger.LogInformation("                            SerilogController                                ");
            _logger.LogInformation("#############################################################################");
            _logger.LogInformation($"Logger type: {_logger.GetType().FullName}");
        }


        [HttpGet("log")]
        public IActionResult LogTeste()
        {
            _logger.LogInformation("#############################################################################");
            _logger.LogInformation("                              Rota: LogTeste                                 ");
            _logger.LogInformation("#############################################################################");

            _logger.LogInformation("-----------------------------------------------------------------------------");
            _logger.LogInformation("SerilogController: iniciado");
            _logger.LogWarning("SerilogController: Chamou endpoint /api/teste/log");
            _logger.LogInformation("-----------------------------------------------------------------------------");

            _logger.LogWarning("-----------------------------------------------------------------------------");
            _logger.LogWarning("SerilogController: iniciou com um warning");
            _logger.LogWarning("SerilogController: Exemplo de warning");
            _logger.LogWarning("-----------------------------------------------------------------------------");

            _logger.LogError("-----------------------------------------------------------------------------");
            _logger.LogError("SerilogController: iniciou com erro simulado");
            _logger.LogWarning("SerilogController: Exemplo de erro");
            _logger.LogError("-----------------------------------------------------------------------------");

            _logger.LogError("-----------------------------------------------------------------------------");
            _logger.LogInformation($"Logger type: {_logger.GetType().FullName}");
            _logger.LogError("-----------------------------------------------------------------------------");


            return Ok("Logs enviados!");
        }



        [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "Buscando usuário com id {id}")]
        partial void LogBuscarUsuario(int? id);


        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int? id, CancellationToken cancellationToken)
        {
           // SerilogConfig.Info("Iniciando execução...");
            _logger.LogTrace("Iniciando busca de usuário. Parâmetro id recebido: {id}", id);
            _logger.LogDebug("Preparando consulta para o id {id} no método {Method}", id, nameof(ObterPorId));

            _logger.LogInformation("Iniciando busca de usuário...");
            _logger.LogInformation("Buscando usuário com id {id}!", id);
            LogBuscarUsuario(id);

            var usuario = new UsuarioDto();
           // var aaaa = usuario.ObterUsuarios().ToList();

            if (usuario == null)
            {
                _logger.LogWarning("Nenhum usuário encontrado com id {id}", id);
                _logger.LogError("Erro: Falha ao localizar o usuário no banco de dados para o id {id}", id);

                // Exemplo de uso do Critical (falha grave)
                if (id > 20) // Simulação de erro crítico
                {
                    _logger.LogCritical("Erro crítico: Usuário com id {id} causou falha no sistema", id);
                }
              //  SerilogConfig.Error("Processo finalizado!");
                return NotFound();
            }



            _logger.LogInformation("Usuário {nome} encontrado!", usuario.Nome);
            _logger.LogInformation("Processo finalizado com sucesso!");
          //  SerilogConfig.Success("Processo finalizado com sucesso!");
            return Ok(usuario);
        }


        [HttpGet("trace")]
        public IActionResult TestTrace()
        {
            _logger.LogTrace("Mensagem TRACE: Detalhes muito técnicos e verbosos para diagnóstico fino.");
            return Ok("Log TRACE registrado");
        }

        [HttpGet("debug")]
        public IActionResult TestDebug()
        {
            _logger.LogDebug("Mensagem DEBUG: Usado para depuração e análise durante o desenvolvimento.");
            return Ok("Log DEBUG registrado");
        }

        [HttpGet("info")]
        public IActionResult TestInformation()
        {
            _logger.LogInformation("Mensagem INFORMATION: Informações gerais do fluxo da aplicação.");
            return Ok("Log INFORMATION registrado");
        }

        [HttpGet("warning")]
        public IActionResult TestWarning()
        {
            _logger.LogWarning("Mensagem WARNING: Algo inesperado aconteceu, mas o sistema continua funcionando.");
            return Ok("Log WARNING registrado");
        }

        [HttpGet("error")]
        public IActionResult TestError()
        {
            _logger.LogError("Mensagem ERROR: Ocorreu um erro que precisa ser tratado.");
            return StatusCode(500, "Log ERROR registrado");
        }

        [HttpGet("critical")]
        public IActionResult TestCritical()
        {
            _logger.LogCritical("Mensagem CRITICAL: Erro grave que pode interromper o sistema.");
            return StatusCode(500, "Log CRITICAL registrado");
        }

    }
}
