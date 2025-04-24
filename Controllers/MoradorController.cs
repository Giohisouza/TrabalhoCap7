using AutoMapper;
using Gestao.Residuos.Models;
using Gestao.Residuos.Services;
using Gestao.Residuos.ViewModel.Caminhao;
using Gestao.Residuos.ViewModel.Morador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao.Residuos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoradorController : ControllerBase
    {
        private readonly IMoradorService _service;
        private readonly IMapper _mapper;

        public MoradorController(IMoradorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<MoradorPaginacaoViewModel>> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var clientes = _service.ListarMoradores(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<MoradorViewModel>>(clientes);

            var viewModel = new MoradorPaginacaoViewModel
            {
                Morador = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult<MoradorViewModel> Get(int id)
        {
            var morador = _service.ObterMoradoresPorId(id);
            if (morador == null)
                return NotFound();

            var viewModel = _mapper.Map<MoradorViewModel>(morador);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Post([FromBody] MoradorCreateViewModel viewModel)
        {
            var morador = _mapper.Map<MoradorModel>(viewModel);
            _service.CriarMoradores(morador);
            return CreatedAtAction(nameof(Get), new { id = morador.Id }, viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Put(int id, [FromBody] MoradorUpdateViewModel viewModel)
        {
            var MoradorExiste = _service.ObterMoradoresPorId(id);
            if (MoradorExiste == null)
                return NotFound();

            _mapper.Map(viewModel, MoradorExiste);
            _service.AtualizarMoradores(MoradorExiste);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            _service.DeletarMoradores(id);
            return NoContent();
        }


    }
}
