using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly ICaminhaoRepository _repository;

        public CaminhaoService(ICaminhaoRepository repository)
        {
            _repository = repository;
        }

        public void AtualizarCaminhoes(CaminhaoModel Caminhao) => _repository.Update(Caminhao);

        public void CriarCaminhoes(CaminhaoModel Caminhao) => _repository.Add(Caminhao);

        public void DeletarCaminhoes(int id)
        {
            var Caminhao = _repository.GetById(id);
            if (Caminhao != null)
            {
                _repository.Delete(Caminhao);
            }
        }

        public IEnumerable<CaminhaoModel> ListarCaminhoes() => _repository.GetAll();

        public IEnumerable<CaminhaoModel> ListarCaminhoes(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public CaminhaoModel ObterCaminhoesPorId(int id) => _repository.GetById(id);
    }
}
