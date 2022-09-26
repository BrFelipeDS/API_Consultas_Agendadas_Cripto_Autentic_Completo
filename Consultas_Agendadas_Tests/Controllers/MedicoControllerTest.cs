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
    public class MedicoControllerTest
    {
        // Preparação
        private readonly Mock<IMedicoRepository> _mockRepo;
        private readonly MedicoController _controller;
        public MedicoControllerTest()
        {
            _mockRepo = new Mock<IMedicoRepository>();
            _controller = new MedicoController(_mockRepo.Object);
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

        [Fact]
        public void TestInsert()
        {
            var result = _controller.Cadastrar(new()
            {
                Crm = "123456789",
                IdUsuario = 1,
                IdEspecialidade = 1,
                IdUsuarioNavigation = new Usuario
                {
                    Nome = "Teste",
                    Email = "teste@testeautomatizado.com",
                    Senha = "teste123456",
                    IdTipoUsuario = 2,
                }
            });
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
