namespace Gestao.Residuos.Models
{
    public class RotaModel
    {
        public int Id { get; set; }
        public int CaminhaoId { get; set; }
        public CaminhaoModel Caminhao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public List<string> Paradas { get; set; } // Lista de coordenadas GPS
    }
}
