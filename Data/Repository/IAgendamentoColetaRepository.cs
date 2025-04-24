using Gestao.Residuos.Models;

namespace Gestao.Residuos.Data.Repository
{
    public interface IAgendamentoColetaRepository
    {
        IEnumerable<AgendamentoColetaModel> GetAll();
        IEnumerable<AgendamentoColetaModel> GetAll(int page, int size);
        AgendamentoColetaModel GetById(int id);
        void Add(AgendamentoColetaModel AgendamentoColeta);
        void Update(AgendamentoColetaModel AgendamentoColeta);
        void Delete(AgendamentoColetaModel AgendamentoColeta);
    }
}
