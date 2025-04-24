using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface ICaminhaoService
    {
        IEnumerable<CaminhaoModel> ListarCaminhoes();
        IEnumerable<CaminhaoModel> ListarCaminhoes(int pagina = 0, int tamanho = 10);
        CaminhaoModel ObterCaminhoesPorId(int id);
        void CriarCaminhoes(CaminhaoModel caminhao);
        void AtualizarCaminhoes(CaminhaoModel caminhao);
        void DeletarCaminhoes(int id);
    }
}
