using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface IMoradorService
    {
        IEnumerable<MoradorModel> ListarMoradores();
        IEnumerable<MoradorModel> ListarMoradores(int pagina = 0, int tamanho = 10);
        MoradorModel ObterMoradoresPorId(int id);
        void CriarMoradores(MoradorModel morador);
        void AtualizarMoradores(MoradorModel morador);
        void DeletarMoradores(int id);
    }
}
