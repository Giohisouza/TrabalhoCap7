using Gestao.Residuos.ViewModel.Caminhao;

namespace Gestao.Residuos.ViewModel.Rota
{
    public class RotaViewModel
    {
        public int Id { get; set; }
        public int CaminhaoId { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public List<string> Paradas { get; set; } // Lista de coordenadas GPS


        // Caso deseje exibir dados do caminhão relacionado
        public CaminhaoViewModel Caminhao { get; set; }
    }
}
