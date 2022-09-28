using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _LoginController : ControllerBase
    {

        private readonly ILoginRepository repo;

        public _LoginController(ILoginRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Espaço para login
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="senha">Senha do usuario</param>
        /// <returns>Token de acesso</returns>
        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = repo.Logar(email, senha);
            if (logar == null)
                return Unauthorized( new {msg = "Usuário não encontrado"});

            return Ok(new { token = logar });
        }        
    }
}
