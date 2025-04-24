using Moq;
using AutoMapper;
using Xunit;
using System.Collections.Generic;
using Gestao.Residuos.Services;
using Gestao.Residuos.Models;
using Gestao.Residuos.Controllers;
using Gestao.Residuos.ViewModel.Morador;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Gestao.Residuos.Testes.Controllers
{
    public class MoradorControllerTests
    {
        private readonly Mock<IMoradorService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MoradorController _controller;

        public MoradorControllerTests()
        {
            _serviceMock = new Mock<IMoradorService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new MoradorController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithData()
        {
            // Arrange
            var moradores = new List<MoradorModel>
            {
                new MoradorModel { Id = 1, Nome="João", Endereco="Rua A", Email="joao@test.com", Telefone="1111-1111" },
                new MoradorModel { Id = 2, Nome="Maria", Endereco="Rua B", Email="maria@test.com", Telefone="2222-2222" }
            };

            _serviceMock.Setup(s => s.ListarMoradores(1, 10)).Returns(moradores);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<MoradorViewModel>>(It.IsAny<IEnumerable<MoradorModel>>()))
                .Returns((IEnumerable<MoradorModel> source) => source.Select(mo => new MoradorViewModel
                {
                    Id = mo.Id,
                    Nome = mo.Nome,
                    Endereco = mo.Endereco,
                    Email = mo.Email,
                    Telefone = mo.Telefone
                }));

            // Act
            var actionResult = _controller.Get(1, 10);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var paginacaoVm = okResult.Value as MoradorPaginacaoViewModel;
            Assert.NotNull(paginacaoVm);
            Assert.Equal(2, paginacaoVm.Morador.Count());
            Assert.Equal(1, paginacaoVm.CurrentPage);
            Assert.Equal(10, paginacaoVm.PageSize);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var morador = new MoradorModel { Id = 1, Nome = "Carlos", Endereco = "Rua C", Email = "carlos@test.com", Telefone = "3333-3333" };

            _serviceMock.Setup(s => s.ObterMoradoresPorId(1)).Returns(morador);

            _mapperMock
                .Setup(m => m.Map<MoradorViewModel>(It.IsAny<MoradorModel>()))
                .Returns((MoradorModel source) => new MoradorViewModel
                {
                    Id = source.Id,
                    Nome = source.Nome,
                    Endereco = source.Endereco,
                    Email = source.Email,
                    Telefone = source.Telefone
                });

            // Act
            var actionResult = _controller.Get(1);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModel = okResult.Value as MoradorViewModel;
            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Id);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterMoradoresPorId(99)).Returns((MoradorModel)null);

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
            var createViewModel = new MoradorCreateViewModel
            {
                Nome = "Ana",
                Endereco = "Rua D",
                Email = "ana@test.com",
                Telefone = "4444-4444"
            };

            var moradorCriado = new MoradorModel
            {
                Id = 10,
                Nome = createViewModel.Nome,
                Endereco = createViewModel.Endereco,
                Email = createViewModel.Email,
                Telefone = createViewModel.Telefone
            };

            _mapperMock
                .Setup(m => m.Map<MoradorModel>(It.IsAny<MoradorCreateViewModel>()))
                .Returns(moradorCriado);

            _serviceMock
                .Setup(s => s.CriarMoradores(moradorCriado))
                .Callback(() => { moradorCriado.Id = 10; });

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
            var updateViewModel = new MoradorUpdateViewModel
            {
                Nome = "Joana",
                Endereco = "Rua E",
                Email = "joana@test.com",
                Telefone = "5555-5555"
            };

            var moradorExistente = new MoradorModel
            {
                Id = 5,
                Nome = "Antigo",
                Endereco = "Rua Velha",
                Email = "antigo@test.com",
                Telefone = "9999-9999"
            };

            _serviceMock.Setup(s => s.ObterMoradoresPorId(5))
                .Returns(moradorExistente);

            // Act
            var actionResult = _controller.Put(5, updateViewModel);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);

            _mapperMock.Verify(m => m.Map(updateViewModel, moradorExistente), Times.Once);
            _serviceMock.Verify(s => s.AtualizarMoradores(moradorExistente), Times.Once);
        }

        [Fact]
        public void Put_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterMoradoresPorId(999))
                .Returns((MoradorModel)null);

            var updateViewModel = new MoradorUpdateViewModel
            {
                Nome = "Inexistente",
                Endereco = "Rua inexistente",
                Email = "inex@test.com",
                Telefone = "0000-0000"
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
            _serviceMock.Setup(s => s.DeletarMoradores(10));

            // Act
            var actionResult = _controller.Delete(10);
            var noContentResult = actionResult as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
            _serviceMock.Verify(s => s.DeletarMoradores(10), Times.Once);
        }
    }
}
