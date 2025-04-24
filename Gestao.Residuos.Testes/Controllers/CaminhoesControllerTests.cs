using Moq;
using AutoMapper;
using Xunit;
using System.Collections.Generic;
using Gestao.Residuos.Services;
using Gestao.Residuos.Models;
using Gestao.Residuos.Controllers;
using Gestao.Residuos.ViewModel.Caminhao;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Gestao.Residuos.Testes.Controllers
{
    public class CaminhoesControllerTests
    {
        private readonly Mock<ICaminhaoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CaminhoesController _controller;

        public CaminhoesControllerTests()
        {
            _serviceMock = new Mock<ICaminhaoService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CaminhoesController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithData()
        {
            // Arrange
            var caminhoes = new List<CaminhaoModel>
            {
                new CaminhaoModel { Id = 1, Placa = "ABC-1234", Modelo = "Caminhao X", CapacidadeMaxima = 10.5 },
                new CaminhaoModel { Id = 2, Placa = "XYZ-9876", Modelo = "Caminhao Y", CapacidadeMaxima = 15.0 }
            };

            _serviceMock.Setup(s => s.ListarCaminhoes(1, 10)).Returns(caminhoes);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CaminhaoViewModel>>(It.IsAny<IEnumerable<CaminhaoModel>>()))
                .Returns((IEnumerable<CaminhaoModel> source) => source.Select(c => new CaminhaoViewModel
                {
                    Id = c.Id,
                    Placa = c.Placa,
                    Modelo = c.Modelo,
                    CapacidadeMaxima = (decimal)c.CapacidadeMaxima
                }));

            // Act
            var actionResult = _controller.Get(1, 10);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var paginacaoVm = okResult.Value as CaminhaoPaginacaoViewModel;
            Assert.NotNull(paginacaoVm);
            Assert.Equal(2, paginacaoVm.Caminhao.Count());
            Assert.Equal(1, paginacaoVm.CurrentPage);
            Assert.Equal(10, paginacaoVm.PageSize);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var caminhao = new CaminhaoModel { Id = 1, Placa = "ABC-1234", Modelo = "Caminhao X", CapacidadeMaxima = 10.5 };
            _serviceMock.Setup(s => s.ObterCaminhoesPorId(1)).Returns(caminhao);

            _mapperMock
                .Setup(m => m.Map<CaminhaoViewModel>(It.IsAny<CaminhaoModel>()))
                .Returns((CaminhaoModel source) => new CaminhaoViewModel
                {
                    Id = source.Id,
                    Placa = source.Placa,
                    Modelo = source.Modelo,
                    CapacidadeMaxima = (decimal)source.CapacidadeMaxima
                });

            // Act
            var actionResult = _controller.Get(1);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModel = okResult.Value as CaminhaoViewModel;
            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Id);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterCaminhoesPorId(99)).Returns((CaminhaoModel)null);

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
            var createViewModel = new CaminhaoCreateViewModel
            {
                Placa = "LMN-5555",
                Modelo = "Caminhao Z",
                CapacidadeMaxima = 20.0m
            };

            var caminhaoCriado = new CaminhaoModel
            {
                Id = 10,
                Placa = createViewModel.Placa,
                Modelo = createViewModel.Modelo,
                CapacidadeMaxima = (double)createViewModel.CapacidadeMaxima
            };

            _mapperMock
                .Setup(m => m.Map<CaminhaoModel>(It.IsAny<CaminhaoCreateViewModel>()))
                .Returns(caminhaoCriado);

            _serviceMock
                .Setup(s => s.CriarCaminhoes(caminhaoCriado))
                .Callback(() => { caminhaoCriado.Id = 10; });

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
            var updateViewModel = new CaminhaoUpdateViewModel
            {
                Placa = "ZZZ-9999",
                Modelo = "Caminhao W",
                CapacidadeMaxima = 25.0m
            };

            var caminhaoExistente = new CaminhaoModel
            {
                Id = 5,
                Placa = "OLD-1111",
                Modelo = "Old Truck",
                CapacidadeMaxima = 5.0
            };

            _serviceMock.Setup(s => s.ObterCaminhoesPorId(5))
                .Returns(caminhaoExistente);

            // Act
            var actionResult = _controller.Put(5, updateViewModel);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);

            _mapperMock.Verify(m => m.Map(updateViewModel, caminhaoExistente), Times.Once);
            _serviceMock.Verify(s => s.AtualizarCaminhoes(caminhaoExistente), Times.Once);
        }

        [Fact]
        public void Put_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterCaminhoesPorId(999))
                .Returns((CaminhaoModel)null);

            var updateViewModel = new CaminhaoUpdateViewModel
            {
                Placa = "AAA-0000",
                Modelo = "Non-Existent Truck",
                CapacidadeMaxima = 15.0m
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
            _serviceMock.Setup(s => s.DeletarCaminhoes(10));

            // Act
            var actionResult = _controller.Delete(10);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
            _serviceMock.Verify(s => s.DeletarCaminhoes(10), Times.Once);
        }
    }
}
