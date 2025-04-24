using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public class MoradorService : IMoradorService
    {

        private readonly IMoradorRepository _repository;

        public MoradorService(IMoradorRepository repository)
        {
            _repository = repository;
        }
        public void AtualizarMoradores(MoradorModel morador) => _repository.Update(morador);

        public void CriarMoradores(MoradorModel morador) => _repository.Add(morador);

        public void DeletarMoradores(int id)
        {
            var Caminhao = _repository.GetById(id);
            if (Caminhao != null)
            {
                _repository.Delete(Caminhao);
            }
        }

        public IEnumerable<MoradorModel> ListarMoradores() => _repository.GetAll();

        public IEnumerable<MoradorModel> ListarMoradores(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public MoradorModel ObterMoradoresPorId(int id) => _repository.GetById(id);
    }
}
