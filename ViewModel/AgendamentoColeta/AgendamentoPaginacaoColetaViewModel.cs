using Gestao.Residuos.ViewModel.Caminhao;
using Gestao.Residuos.ViewModel.Morador;

namespace Gestao.Residuos.ViewModel.AgendamentoColeta
{
    public class AgendamentoPaginacaoColetaViewModel
    {
        public IEnumerable<AgendamentoColetaViewModel> Agendamento { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Agendamento.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Cliente?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Cliente?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";
    }
}
