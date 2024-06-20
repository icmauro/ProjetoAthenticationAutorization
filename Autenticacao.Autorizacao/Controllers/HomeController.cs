using Autenticacao.Autorizacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Autenticacao.Autorizacao.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User?.Identity?.Name}" ;

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles ="employee,manager")]
        public string Employee() => $"Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => $"Manager";

    }
}
