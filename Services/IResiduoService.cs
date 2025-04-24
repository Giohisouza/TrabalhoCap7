using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface IResiduoService
    {
        IEnumerable<ResiduoModel> ListarResiduos();
        IEnumerable<ResiduoModel> ListarResiduos(int pagina = 0, int tamanho = 10);
        ResiduoModel ObterResiduosPorId(int id);
        void CriarResiduos(ResiduoModel residuo);
        void AtualizarResiduos(ResiduoModel residuo);
        void DeletarResiduos(int id);
    }
}
