namespace Serilog.Aplicacao.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public static List<UsuarioDto> ObterUsuarios()
        {
            return Enumerable.Range(1, 20)
                .Select(i => new UsuarioDto
                {
                    Id = i,
                    Nome = $"Usuário {i}"
                })
                .ToList();
        }
    }
}
