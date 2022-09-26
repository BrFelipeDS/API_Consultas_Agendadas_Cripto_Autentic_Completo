using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Injeção de dependência do repositório

        private readonly IUsuarioRepository repositorio;

        public UsuarioController(IUsuarioRepository _repositorio)
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

        /// <summary>
        /// Seleciona apenas 1 objeto com o Id especificado
        /// </summary>
        /// <param name="id">Id do objeto a ser selecionado</param>
        /// <returns>Objeto com o Id inserido</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
                }

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

        /// <summary>
        /// Altera apenas informações específicas do objeto
        /// </summary>
        /// <param name="id">Id do objeto a ser alterado</param>
        /// <param name="patch">Informações que serão alteradas no objeto destino</param>
        /// <returns>Novo objeto com as alterações realizadas</returns>
        [HttpPatch("{id}")]
        public IActionResult AlterarParcialmente(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch is null)
                {
                    return BadRequest(new { Message = "Não houve alterações no objeto" });
                }

                var usuario = repositorio.GetById(id);

                if (usuario is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
                }

                repositorio.UpdateParcial(patch, usuario);

                return Ok(usuario);
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
