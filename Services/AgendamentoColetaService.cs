using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public class AgendamentoColetaService : IAgendamentoColetaService
    {

        private readonly IAgendamentoColetaRepository _repository;

        public AgendamentoColetaService(IAgendamentoColetaRepository repository)
        {
            _repository = repository;
        }

        public void AtualizarAgendamentoColeta(AgendamentoColetaModel AgendamentoColeta) => _repository.Update(AgendamentoColeta);

        public void CriarAgendamentoColeta(AgendamentoColetaModel AgendamentoColeta) => _repository.Add(AgendamentoColeta);

        public void DeletarAgendamentoColeta(int id)
        {
            var AgendamentoColeta = _repository.GetById(id);
            if (AgendamentoColeta != null)
            {
                _repository.Delete(AgendamentoColeta);
            }
        }

        public IEnumerable<AgendamentoColetaModel> ListarAgendamentoColeta() => _repository.GetAll();

        public IEnumerable<AgendamentoColetaModel> ListarAgendamentoColeta(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public AgendamentoColetaModel ObterAgendamentoColetaPorId(int id) => _repository.GetById(id);
    }
}
