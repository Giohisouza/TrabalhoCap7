using System;
using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Gestao.Residuos.Controllers;
using Gestao.Residuos.Services;
using Gestao.Residuos.Models;
using Gestao.Residuos.ViewModel.AgendamentoColeta;

namespace Gestao.Residuos.Testes.Controllers
{
    public class AgendarControllerTests
    {
        private readonly Mock<IAgendamentoColetaService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AgendarController _controller;

        public AgendarControllerTests()
        {
            _serviceMock = new Mock<IAgendamentoColetaService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new AgendarController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithData()
        {
            // Arrange
            var agendamentos = new List<AgendamentoColetaModel>
            {
                new AgendamentoColetaModel { Id = 1, TipoResiduo="Plastico", CapacidadeAtualRecipiente=10, MoradorId=1, DataColeta=DateTime.Now },
                new AgendamentoColetaModel { Id = 2, TipoResiduo="Vidro", CapacidadeAtualRecipiente=5, MoradorId=2, DataColeta=DateTime.Now }
            };

            _serviceMock
                .Setup(s => s.ListarAgendamentoColeta(1, 10))
                .Returns(agendamentos);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<AgendamentoColetaViewModel>>(It.IsAny<IEnumerable<AgendamentoColetaModel>>()))
                .Returns((IEnumerable<AgendamentoColetaModel> source) => source.Select(a => new AgendamentoColetaViewModel
                {
                    Id = a.Id,
                    TipoResiduo = a.TipoResiduo,
                    CapacidadeAtualRecipiente = a.CapacidadeAtualRecipiente,
                    MoradorId = a.MoradorId,
                    DataColeta = a.DataColeta
                }));

            // Act
            var actionResult = _controller.Get(1, 10);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModel = okResult.Value as AgendamentoPaginacaoColetaViewModel;
            Assert.NotNull(viewModel);
            Assert.Equal(2, viewModel.Agendamento.Count());
            Assert.Equal(1, viewModel.CurrentPage);
            Assert.Equal(10, viewModel.PageSize);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var agendamento = new AgendamentoColetaModel { Id = 1, TipoResiduo = "Papel", CapacidadeAtualRecipiente = 3, MoradorId = 1, DataColeta = DateTime.Now };

            _serviceMock
                .Setup(s => s.ObterAgendamentoColetaPorId(1))
                .Returns(agendamento);

            _mapperMock
                .Setup(m => m.Map<AgendamentoColetaViewModel>(It.IsAny<AgendamentoColetaModel>()))
                .Returns((AgendamentoColetaModel source) => new AgendamentoColetaViewModel
                {
                    Id = source.Id,
                    TipoResiduo = source.TipoResiduo,
                    CapacidadeAtualRecipiente = source.CapacidadeAtualRecipiente,
                    MoradorId = source.MoradorId,
                    DataColeta = source.DataColeta
                });

            // Act
            var actionResult = _controller.Get(1);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModel = okResult.Value as AgendamentoColetaViewModel;
            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Id);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.ObterAgendamentoColetaPorId(99))
                .Returns((AgendamentoColetaModel)null);

            // Act
            var actionResult = _controller.Get(99);
            var notFoundResult = actionResult.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void Post_ShouldCreateAndReturnCreatedAt()
        {
            // Arrange
            var createViewModel = new AgendamentoCreateColetaViewModel
            {
                TipoResiduo = "Metal",
                CapacidadeAtualRecipiente = 20,
                MoradorId = 1,
                DataColeta = DateTime.Now
            };

            var agendamentoCriado = new AgendamentoColetaModel
            {
                Id = 10,
                TipoResiduo = createViewModel.TipoResiduo,
                CapacidadeAtualRecipiente = createViewModel.CapacidadeAtualRecipiente,
                MoradorId = createViewModel.MoradorId,
                DataColeta = createViewModel.DataColeta
            };

            _mapperMock
                .Setup(m => m.Map<AgendamentoColetaModel>(It.IsAny<AgendamentoCreateColetaViewModel>()))
                .Returns(agendamentoCriado);

            _serviceMock
                .Setup(s => s.CriarAgendamentoColeta(agendamentoCriado))
                .Callback(() => { agendamentoCriado.Id = 10; });

            // Act
            var actionResult = _controller.Post(createViewModel);
            var createdAtResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(createdAtResult);
            Assert.Equal("Get", createdAtResult.ActionName);
            Assert.Equal(10, createdAtResult.RouteValues["id"]);
            Assert.Equal(createViewModel, createdAtResult.Value);
        }

        [Fact]
        public void Put_ShouldUpdateAndReturnNoContent_WhenExists()
        {
            // Arrange
            var updateViewModel = new AgendamentoUpdateColetaViewModel
            {
                TipoResiduo = "Alumínio",
                CapacidadeAtualRecipiente = 30,
                MoradorId = 2,
                DataColeta = DateTime.Now
            };

            var agendamentoExistente = new AgendamentoColetaModel
            {
                Id = 5,
                TipoResiduo = "Plastico",
                CapacidadeAtualRecipiente = 10,
                MoradorId = 1,
                DataColeta = DateTime.Now
            };

            _serviceMock.Setup(s => s.ObterAgendamentoColetaPorId(5))
                .Returns(agendamentoExistente);

            // Act
            var actionResult = _controller.Put(5, updateViewModel);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);

            _mapperMock.Verify(m => m.Map(updateViewModel, agendamentoExistente), Times.Once);
            _serviceMock.Verify(s => s.AtualizarAgendamentoColeta(agendamentoExistente), Times.Once);
        }

        [Fact]
        public void Put_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterAgendamentoColetaPorId(999))
                .Returns((AgendamentoColetaModel)null);

            var updateViewModel = new AgendamentoUpdateColetaViewModel
            {
                TipoResiduo = "Qualquer",
                CapacidadeAtualRecipiente = 10,
                MoradorId = 1,
                DataColeta = DateTime.Now
            };

            // Act
            var actionResult = _controller.Put(999, updateViewModel);
            var notFound = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFound);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenExists()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeletarAgendamentoColeta(10));

            // Act
            var actionResult = _controller.Delete(10);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
            _serviceMock.Verify(s => s.DeletarAgendamentoColeta(10), Times.Once);
        }
    }
}
