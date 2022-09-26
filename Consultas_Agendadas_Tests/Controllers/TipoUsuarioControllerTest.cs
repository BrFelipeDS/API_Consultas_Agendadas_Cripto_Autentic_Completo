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
    public class TipoUsuarioControllerTest
    {
        // Preparação
        private readonly Mock<ITipoUsuarioRepository> _mockRepo;
        private readonly TipoUsuarioController _controller;
        public TipoUsuarioControllerTest()
        {
            _mockRepo = new Mock<ITipoUsuarioRepository>();
            _controller = new TipoUsuarioController(_mockRepo.Object);
        }

        [Fact]
        public void TestActionResultReturnOk()
        {

            // Execução
            var result = _controller.Listar();
            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TestGetAll()
        {
            // Execução - Act
            var actionResult = _controller.Listar();
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Value = new List<Consulta>();
            // Retorno
            Assert.IsAssignableFrom<List<Consulta>>(okObjectResult.Value);

        }

        [Fact]
        public void TestStatusCodeSuccess()
        {
            // Execução - Act
            var actionResult = _controller.Listar();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestActionResultNotNull()
        {
            // Execução - Act
            var actionResult = _controller.Listar();
            // Retorno
            Assert.NotNull(actionResult);
        }
    }
}
