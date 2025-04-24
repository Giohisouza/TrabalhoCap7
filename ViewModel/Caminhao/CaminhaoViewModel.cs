namespace Gestao.Residuos.ViewModel.Caminhao
{
    public class CaminhaoViewModel
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public decimal CapacidadeMaxima { get; set; }
        public string LocalizacaoAtual { get; set; } // Coordenadas GPS
    }
}
