namespace Autenticacao.Autorizacao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public string? Roles { get; set; }

    }
}
