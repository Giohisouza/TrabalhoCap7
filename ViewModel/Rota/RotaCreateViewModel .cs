namespace Gestao.Residuos.ViewModel.Rota
{
    public class RotaCreateViewModel
    {
        public int CaminhaoId { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public List<string> Paradas { get; set; } // Lista de coordenadas GPS

    }
}
