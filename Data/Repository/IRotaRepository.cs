using Gestao.Residuos.Models;

namespace Gestao.Residuos.Data.Repository
{
    public interface IRotaRepository
    {
        IEnumerable<RotaModel> GetAll();
        IEnumerable<RotaModel> GetAll(int page, int size);
        RotaModel GetById(int id);
        void Add(RotaModel rota);
        void Update(RotaModel rota);
        void Delete(RotaModel rota);
    }
}
