using Autenticacao.Autorizacao.Models;
using Autenticacao.Autorizacao.Repositories;
using Autenticacao.Autorizacao.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Autorizacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<dynamic>> AutenticateAsync([FromBody] Usuario usuario)
        { 
            var user = UsuarioRepositorio.GetUsuario(usuario.Nome, usuario.Password);

            if(user is null)
                return NotFound("Usuário ou Senha Inválidos");

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };

        }

        [HttpGet("Get")]
        public ActionResult Get()
        {
            return Ok("Teste Ok");

        }
    }
}
