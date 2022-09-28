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
        /// Espaço para login de Pacientes
        /// </summary>
        /// <param name="email">Email do paciente</param>
        /// <param name="senha">Senha do paciente</param>
        /// <returns>Token de acesso</returns>
        [HttpPost("Login_Paciente")]
        public IActionResult LogarPaciente(string email, string senha)
        {
            var logar = repo.LogarPaciente(email, senha);
            if (logar == null)
                return Unauthorized( new {msg = "Usuário não autorizado"});

            return Ok(new { token = logar });
        }

        /// <summary>
        /// Espaço para login de Medicos
        /// </summary>
        /// <param name="email">Email do medico</param>
        /// <param name="senha">Senha do medico</param>
        /// <returns>Token de acesso</returns>
        [HttpPost("Login_Medico")]
        public IActionResult LogarMedico(string email, string senha)
        {
            var logar = repo.LogarMedico(email, senha);
            if (logar == null)
                return Unauthorized(new { msg = "Usuário não autorizado" });

            return Ok(new { token = logar });
        }
    }
}
