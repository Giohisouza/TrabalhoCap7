using Gestao.Residuos.Models;

namespace Gestao.Residuos.Data.Repository
{
    public interface IMoradorRepository
    {
        IEnumerable<MoradorModel> GetAll();
        IEnumerable<MoradorModel> GetAll(int page, int size);
        MoradorModel GetById(int id);
        void Add(MoradorModel morador);
        void Update(MoradorModel morador);
        void Delete(MoradorModel morador);
    }
}
