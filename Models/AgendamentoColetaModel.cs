namespace Gestao.Residuos.Models
{
    public class AgendamentoColetaModel
    {
        public int Id { get; set; }
        public int MoradorId { get; set; }
        public MoradorModel Morador { get; set; }
        public DateTime DataColeta { get; set; }
        public string TipoResiduo { get; set; } // Exemplo: "Orgânico", "Reciclável"
        public double CapacidadeAtualRecipiente { get; set; } // Em litros
    }
}
