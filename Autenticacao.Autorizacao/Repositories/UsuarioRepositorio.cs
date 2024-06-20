using Autenticacao.Autorizacao.Models;

namespace Autenticacao.Autorizacao.Repositories
{
    public static class UsuarioRepositorio
    {
        public static Usuario GetUsuario(string nome, string password)
        { 
            var user = new List<Usuario>()
            {
                new Usuario() { Id= 1, Nome = "batman", Password = "batman", Roles = "manager" },
                new Usuario() { Id = 1, Nome = "robin", Password = "robin", Roles = "employee" }
            };

            //user.Add(new Usuario() { Id = 1, Nome = "batman", Password = "batman", Roles = "" });

            return user
                    .FirstOrDefault(x => string.Equals(x.Nome, nome, StringComparison.OrdinalIgnoreCase) && x.Password == password);
        }
    }
}
