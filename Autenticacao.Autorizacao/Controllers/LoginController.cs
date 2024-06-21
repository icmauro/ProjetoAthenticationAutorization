using Autenticacao.Autorizacao.Models;
using Autenticacao.Autorizacao.Repositories;
using Autenticacao.Autorizacao.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;

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

            if (user is null)
                return NotFound("Usuário ou Senha Inválidos");

            var token = TokenService.GenerateToken(user);
            var refreshToken = TokenService.GenerateRefreshToken();

            TokenService.SaveRefreshToken(usuario.Nome, refreshToken);

            user.Password = "";

            return new
            {
                user,
                token,
                refreshToken
            };

        }

        [HttpPost("refresh")]
        public IActionResult Refresh(string token, string refreshToken)
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(token);

            var nome = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(nome);

            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newJwtToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();

            TokenService.DeleteRefreshToken(nome, refreshToken);
            TokenService.SaveRefreshToken(nome, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken

            });
        }



        [HttpGet("Get")]
        public ActionResult Get()
        {
            return Ok("Teste Ok");

        }
    }
}
