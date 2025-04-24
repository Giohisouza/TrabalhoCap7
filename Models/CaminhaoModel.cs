namespace Gestao.Residuos.Models
{
    public class CaminhaoModel
    {
        public int Id { get; set; }
        public string? Placa { get; set; }
        public string? Modelo { get; set; }
        public double? CapacidadeMaxima { get; set; } // Em toneladas
        public string LocalizacaoAtual { get; set; } // Coordenadas GPS
    }
}
