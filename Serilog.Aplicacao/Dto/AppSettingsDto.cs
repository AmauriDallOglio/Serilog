namespace Serilog.Aplicacao.Dto
{
    public class AppSettingsDto
    {
        public ConfiguracoesLogDto ConfiguracoesLog { get; set; } = new();
    }

    public class ConfiguracoesLogDto
    {
        /// <summary>
        /// Liga ou desliga o logging da aplicação.
        /// </summary>
        public bool Ativado { get; set; } = true;

        /// <summary>
        /// Nível mínimo de log global (Console + Debug).
        /// Opções válidas: "Detalhado", "Informacoes", "Aviso", "Erro"
        /// </summary>
        public string Nivel { get; set; } = "Informacoes";

        /// <summary>
        /// Nível mínimo de log exclusivo para o Fluxo de log do Azure App Service.
        /// Se não informado, usa o mesmo valor de Nivel.
        /// Opções válidas: "Detalhado", "Informacoes", "Aviso", "Erro"
        /// </summary>
        public string? NivelAzure { get; set; } = null;
    }

}
