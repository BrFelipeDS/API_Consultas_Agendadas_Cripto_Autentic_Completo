using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuarioRepository repositorio;

        public TipoUsuarioController(ITipoUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Lista todos os objetos presentes no banco de dados
        /// </summary>
        /// <returns>Lista de todos os objetos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.GetAll();
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }

        }
    }
}
