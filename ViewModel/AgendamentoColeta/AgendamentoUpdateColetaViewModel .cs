namespace Gestao.Residuos.ViewModel.AgendamentoColeta
{
    public class AgendamentoUpdateColetaViewModel
    {
        public DateTime DataColeta { get; set; }
        public string TipoResiduo { get; set; }
        public double CapacidadeAtualRecipiente { get; set; }
        public int MoradorId { get; set; }
    }
}
