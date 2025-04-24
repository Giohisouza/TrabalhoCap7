using Moq;
using AutoMapper;
using Xunit;
using System.Collections.Generic;
using Gestao.Residuos.Services;
using Gestao.Residuos.Models;
using Gestao.Residuos.Controllers;
using Gestao.Residuos.ViewModel.Rota;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Gestao.Residuos.Testes.Controllers
{
    public class RotaControllerTests
    {
        private readonly Mock<IRotaService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RotaController _controller;

        public RotaControllerTests()
        {
            _serviceMock = new Mock<IRotaService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new RotaController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithData()
        {
            // Arrange
            var rotas = new List<RotaModel>
            {
                new RotaModel { Id = 1, CaminhaoId=1, DataHoraInicio=DateTime.Now, DataHoraFim=DateTime.Now.AddHours(1) },
                new RotaModel { Id = 2, CaminhaoId=2, DataHoraInicio=DateTime.Now, DataHoraFim=DateTime.Now.AddHours(2) }
            };

            _serviceMock.Setup(s => s.ListarRotas(1, 10)).Returns(rotas);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<RotaViewModel>>(It.IsAny<IEnumerable<RotaModel>>()))
                .Returns((IEnumerable<RotaModel> source) => source.Select(r => new RotaViewModel
                {
                    Id = r.Id,
                    CaminhaoId = r.CaminhaoId,
                    DataHoraInicio = r.DataHoraInicio,
                    DataHoraFim = r.DataHoraFim
                }));

            // Act
            var actionResult = _controller.Get(1, 10);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var paginacaoVm = okResult.Value as RotaPaginacaosViewModel;
            Assert.NotNull(paginacaoVm);
            Assert.Equal(2, paginacaoVm.Rota.Count());
            Assert.Equal(1, paginacaoVm.CurrentPage);
            Assert.Equal(10, paginacaoVm.PageSize);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var rota = new RotaModel { Id = 1, CaminhaoId = 1, DataHoraInicio = DateTime.Now, DataHoraFim = DateTime.Now.AddHours(1) };
            _serviceMock.Setup(s => s.ObterRotasPorId(1)).Returns(rota);

            _mapperMock
                .Setup(m => m.Map<RotaViewModel>(It.IsAny<RotaModel>()))
                .Returns((RotaModel source) => new RotaViewModel
                {
                    Id = source.Id,
                    CaminhaoId = source.CaminhaoId,
                    DataHoraInicio = source.DataHoraInicio,
                    DataHoraFim = source.DataHoraFim
                });

            // Act
            var actionResult = _controller.Get(1);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModel = okResult.Value as RotaViewModel;
            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Id);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterRotasPorId(99)).Returns((RotaModel)null);

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
            var createViewModel = new RotaCreateViewModel
            {
                CaminhaoId = 1,
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddHours(1)
            };

            var rotaCriada = new RotaModel
            {
                Id = 10,
                CaminhaoId = createViewModel.CaminhaoId,
                DataHoraInicio = createViewModel.DataHoraInicio,
                DataHoraFim = createViewModel.DataHoraFim
            };

            _mapperMock
                .Setup(m => m.Map<RotaModel>(It.IsAny<RotaCreateViewModel>()))
                .Returns(rotaCriada);

            _serviceMock
                .Setup(s => s.CriarRrotas(rotaCriada))
                .Callback(() => { rotaCriada.Id = 10; });

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
            var updateViewModel = new RotaUpdateViewModel
            {
                CaminhaoId = 2,
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddHours(2)
            };

            var rotaExistente = new RotaModel
            {
                Id = 5,
                CaminhaoId = 1,
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddHours(1)
            };

            _serviceMock.Setup(s => s.ObterRotasPorId(5))
                .Returns(rotaExistente);

            // Act
            var actionResult = _controller.Put(5, updateViewModel);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);

            _mapperMock.Verify(m => m.Map(updateViewModel, rotaExistente), Times.Once);
            _serviceMock.Verify(s => s.AtualizarRotas(rotaExistente), Times.Once);
        }

        [Fact]
        public void Put_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterRotasPorId(999))
                .Returns((RotaModel)null);

            var updateViewModel = new RotaUpdateViewModel
            {
                CaminhaoId = 2,
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddHours(2)
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
            _serviceMock.Setup(s => s.DeletarRotas(10));

            // Act
            var actionResult = _controller.Delete(10);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
            _serviceMock.Verify(s => s.DeletarRotas(10), Times.Once);
        }
    }
}
