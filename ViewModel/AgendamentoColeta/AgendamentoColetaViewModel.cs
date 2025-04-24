using Gestao.Residuos.ViewModel.Morador;

namespace Gestao.Residuos.ViewModel.AgendamentoColeta
{
    public class AgendamentoColetaViewModel
    {
        public int Id { get; set; }
        public DateTime DataColeta { get; set; }
        public string TipoResiduo { get; set; }
        public double CapacidadeAtualRecipiente { get; set; }
        public int MoradorId { get; set; }

        // Se quiser exibir informações do morador
        public MoradorViewModel Morador { get; set; }
    }
}
