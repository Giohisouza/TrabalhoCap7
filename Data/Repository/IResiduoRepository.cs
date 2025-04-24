using Gestao.Residuos.Models;

namespace Gestao.Residuos.Data.Repository
{
    public interface IResiduoRepository
    {
        IEnumerable<ResiduoModel> GetAll();
        IEnumerable<ResiduoModel> GetAll(int page, int size);
        ResiduoModel GetById(int id);
        void Add(ResiduoModel residuo);
        void Update(ResiduoModel residuo);
        void Delete(ResiduoModel residuo);
    }
}
