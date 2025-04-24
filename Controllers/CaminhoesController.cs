using AutoMapper;
using Gestao.Residuos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestao.Residuos.Models;
using Gestao.Residuos.ViewModel.Caminhao;
using Microsoft.AspNetCore.Authorization;

namespace Gestao.Residuos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaminhoesController : ControllerBase
    {

        private readonly ICaminhaoService _service;
        private readonly IMapper _mapper;

        public CaminhoesController(ICaminhaoService caminhaoService, IMapper mapper)
        {
            _service = caminhaoService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CaminhaoPaginacaoViewModel> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var clientes = _service.ListarCaminhoes(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<CaminhaoViewModel>>(clientes);

            var viewModel = new CaminhaoPaginacaoViewModel
            {
                Caminhao = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public ActionResult<CaminhaoViewModel> Get(int id)
        {
            var Caminhao = _service.ObterCaminhoesPorId(id);
            if (Caminhao == null)
                return NotFound();

            var viewModel = _mapper.Map<CaminhaoViewModel>(Caminhao);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CaminhaoCreateViewModel viewModel)
        {
            var caminhao = _mapper.Map<CaminhaoModel>(viewModel);
            _service.CriarCaminhoes(caminhao);
            return CreatedAtAction(nameof(Get), new { id = caminhao.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CaminhaoUpdateViewModel viewModel)
        {
            var CaminhaoExistente = _service.ObterCaminhoesPorId(id);
            if (CaminhaoExistente == null)
                return NotFound();

            _mapper.Map(viewModel, CaminhaoExistente);
            _service.AtualizarCaminhoes(CaminhaoExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletarCaminhoes(id);
            return NoContent();
        }
    }
}
