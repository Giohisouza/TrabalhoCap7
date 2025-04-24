using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface IRotaService
    {
        IEnumerable<RotaModel> ListarRotas();
        IEnumerable<RotaModel> ListarRotas(int pagina = 0, int tamanho = 10);
        RotaModel ObterRotasPorId(int id);
        void CriarRrotas(RotaModel rota);
        void AtualizarRotas(RotaModel rota);
        void DeletarRotas(int id);
    }
}
