using AutoMapper;
using Gestao.Residuos.Models;
using Gestao.Residuos.Services;
using Gestao.Residuos.ViewModel.Caminhao;
using Gestao.Residuos.ViewModel.Morador;
using Gestao.Residuos.ViewModel.Rota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao.Residuos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ControllerBase
    {
        private readonly IRotaService _service;
        private readonly IMapper _mapper;

        public RotaController(IRotaService rotaService, IMapper mapper)
        {
            _service = rotaService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RotaPaginacaosViewModel>> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var clientes = _service.ListarRotas(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<RotaViewModel>>(clientes);

            var viewModel = new RotaPaginacaosViewModel
            {
                Rota = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RotaViewModel> Get(int id)
        {
            var rota = _service.ObterRotasPorId(id);
            if (rota == null)
                return NotFound();

            var viewModel = _mapper.Map<RotaViewModel>(rota);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromBody] RotaCreateViewModel viewModel)
        {
            var rota = _mapper.Map<RotaModel>(viewModel);
            _service.CriarRrotas(rota);
            return CreatedAtAction(nameof(Get), new { id = rota.Id }, viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Put(int id, [FromBody] RotaUpdateViewModel viewModel)
        {
            var RotaExistente = _service.ObterRotasPorId(id);
            if (RotaExistente == null)
                return NotFound();

            _mapper.Map(viewModel, RotaExistente);
            _service.AtualizarRotas(RotaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _service.DeletarRotas(id);
            return NoContent();
        }
    }
}
