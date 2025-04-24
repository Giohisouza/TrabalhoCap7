namespace Gestao.Residuos.Models
{
    public class ResiduoModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // Exemplo: "Orgânico", "Plástico", "Vidro"
        public string InstrucoesDescarte { get; set; }
    }
}
