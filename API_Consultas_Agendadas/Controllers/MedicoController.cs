using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository repositorio;

        public MedicoController(IMedicoRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Inserção de um objeto no banco de dados. MANTENHA O VALOR DE TODOS OS "Id" COMO "0"!
        /// </summary>
        /// <param name="medico">Objeto completo a ser inserido</param>
        /// <returns>Objeto inserido</returns>
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {
            try
            {
                medico.IdUsuarioNavigation.Senha = BCrypt.Net.BCrypt.HashPassword(medico.IdUsuarioNavigation.Senha);
                var retorno = repositorio.Insert(medico);
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
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
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
        /// Altera um objeto no banco de dados conforme o Id. ATENÇÃO: NO JSON, COLOQUE O VALOR DOS "Id" CONFORME SEUS VALORES NO BANCO DE DADOS!
        /// </summary>
        /// <param name="id">Id do objeto a ser alterado</param>
        /// <param name="medico">O objeto completado que substituirá o existente no banco de dados</param>
        /// <returns>Objeto alterado</returns>
        [Authorize(Roles = "Medico")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (id != medico.Id)
                {
                    return BadRequest(new { Message = "O id indicado deve ser o mesmo id do medico" });
                }

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
                }

                medico.IdUsuarioNavigation.Senha = BCrypt.Net.BCrypt.HashPassword(medico.IdUsuarioNavigation.Senha);

                repositorio.Update(medico);

                return Ok(medico);
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
        [Authorize(Roles = "Medico")]
        [HttpPatch("{id}")]
        public IActionResult AlterarParcialmente(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch is null)
                {
                    return BadRequest(new { Message = "Não houve alterações no objeto" });
                }

                var medico = repositorio.GetById(id);

                if (medico is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
                }

                medico.IdUsuarioNavigation.Senha = BCrypt.Net.BCrypt.HashPassword(medico.IdUsuarioNavigation.Senha);

                repositorio.UpdateParcial(patch, medico);

                return Ok(medico);
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
        /// Deleta um objeto do banco de dados de acordo com o id informado
        /// </summary>
        /// <param name="id">Id do objeto a ser deletado</param>
        /// <returns></returns>
        [Authorize(Roles = "Medico")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var busca = repositorio.GetById(id);

                if (busca is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
                }

                repositorio.Delete(busca);

                return Ok(new {msg = "Excluido com sucesso"});
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
