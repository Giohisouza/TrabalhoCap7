using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface IAgendamentoColetaService
    {
        IEnumerable<AgendamentoColetaModel> ListarAgendamentoColeta();
        IEnumerable<AgendamentoColetaModel> ListarAgendamentoColeta(int pagina = 0, int tamanho = 10);
        AgendamentoColetaModel ObterAgendamentoColetaPorId(int id);
        void CriarAgendamentoColeta(AgendamentoColetaModel cliente);
        void AtualizarAgendamentoColeta(AgendamentoColetaModel cliente);
        void DeletarAgendamentoColeta(int id);
    }
}
