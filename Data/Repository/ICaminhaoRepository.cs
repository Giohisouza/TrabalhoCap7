using Gestao.Residuos.Models;

namespace Gestao.Residuos.Data.Repository
{
    public interface ICaminhaoRepository
    {
        IEnumerable<CaminhaoModel> GetAll();
        IEnumerable<CaminhaoModel> GetAll(int page, int size);
        CaminhaoModel GetById(int id);
        void Add(CaminhaoModel caminhao);
        void Update(CaminhaoModel caminhao);
        void Delete(CaminhaoModel caminhao);
    }
}
