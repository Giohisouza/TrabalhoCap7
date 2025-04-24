namespace Gestao.Residuos.ViewModel.Rota
{
    public class RotaUpdateViewModel
    {
        public int CaminhaoId { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public List<string> Paradas { get; set; } // Lista de coordenadas GPS

    }
}
