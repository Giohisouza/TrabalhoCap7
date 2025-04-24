using AutoMapper;
using Gestao.Residuos.Models;
using Gestao.Residuos.Services;
using Gestao.Residuos.ViewModel.AgendamentoColeta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestao.Residuos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendarController : ControllerBase
    {

        private readonly IAgendamentoColetaService _service;
        private readonly IMapper _mapper;

        public AgendarController(IAgendamentoColetaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult<AgendamentoPaginacaoColetaViewModel> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var clientes = _service.ListarAgendamentoColeta(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AgendamentoColetaViewModel>>(clientes);

            var viewModel = new AgendamentoPaginacaoColetaViewModel
            {
                Agendamento = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult<AgendamentoColetaViewModel> Get(int id)
        {
            var agendamento = _service.ObterAgendamentoColetaPorId(id);
            if (agendamento == null)
                return NotFound();

            var viewModel = _mapper.Map<AgendamentoColetaViewModel>(agendamento);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Post([FromBody] AgendamentoCreateColetaViewModel viewModel)
        {
            var agendamento = _mapper.Map<AgendamentoColetaModel>(viewModel);
            _service.CriarAgendamentoColeta(agendamento);
            return CreatedAtAction(nameof(Get), new { id = agendamento.Id }, viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Put(int id, [FromBody] AgendamentoUpdateColetaViewModel viewModel)
        {
            var AgendamentoExiste = _service.ObterAgendamentoColetaPorId(id);
            if (AgendamentoExiste == null)
                return NotFound();

            _mapper.Map(viewModel, AgendamentoExiste);
            _service.AtualizarAgendamentoColeta(AgendamentoExiste);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            _service.DeletarAgendamentoColeta(id);
            return NoContent();
        }
    }
}
