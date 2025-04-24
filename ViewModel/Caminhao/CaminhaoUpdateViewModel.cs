using System.ComponentModel.DataAnnotations;

namespace Gestao.Residuos.ViewModel.Caminhao
{
    public class CaminhaoUpdateViewModel
    {

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public decimal CapacidadeMaxima { get; set; }
        public string LocalizacaoAtual { get; set; } // Coordenadas GPS

    }

}
