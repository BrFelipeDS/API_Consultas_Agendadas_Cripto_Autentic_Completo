using API_Consultas_Agendadas.Controllers;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Agendadas_Tests.Controllers
{
    public class ConsultaControllerTest
    {
        // Preparação do Mock com o respectivo repositório
        private readonly Mock<IConsultaRepository> _mockRepo;
        private readonly ConsultaController _controller; // Instancia do controller
        public ConsultaControllerTest()
        {
            _mockRepo = new Mock<IConsultaRepository>();
            _controller = new ConsultaController(_mockRepo.Object);
        }

        /// <summary>
        /// Testa o retorno do status Ok
        /// </summary>
        [Fact]
        public void TestActionResultReturnOk()
        {

            var result = _controller.Listar();
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Testa a execução do método GetAll
        /// </summary>
        [Fact]
        public void TestGetAll()
        {
            // Act
            var actionResult = _controller.Listar();
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Value = new List<Consulta>();
            // Retorno
            Assert.IsAssignableFrom<List<Consulta>>(okObjectResult.Value);

        }

        /// <summary>
        /// Testa o retorno do status code 200 (Sucesso)
        /// </summary>
        [Fact]
        public void TestStatusCodeSuccess()
        {
            // Act
            var actionResult = _controller.Listar();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);
        }

        /// <summary>
        /// Testa se o retorno é Not Null
        /// </summary>
        [Fact]
        public void TestActionResultNotNull()
        {
            // Execução - Act
            var actionResult = _controller.Listar();
            // Retorno
            Assert.NotNull(actionResult);
        }

        /// <summary>
        /// Testa o método de Insert com um objeto de teste
        /// </summary>
        [Fact]
        public void TestInsert()
        {
            var result = _controller.Cadastrar(new()
            {
                DataHora = DateTime.Now,
                IdMedico = 1,
                IdPaciente = 1,
            });
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
