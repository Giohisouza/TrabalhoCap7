using Gestao.Residuos.ViewModel.Morador;

namespace Gestao.Residuos.ViewModel.AgendamentoColeta
{
    public class AgendamentoCreateColetaViewModel
    {

        public DateTime DataColeta { get; set; }
        public string TipoResiduo { get; set; }
        public double CapacidadeAtualRecipiente { get; set; }
        public int MoradorId { get; set; }

    }
}
