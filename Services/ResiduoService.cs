using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public class ResiduoService : IResiduoService
    {
        private readonly IResiduoRepository _repository;

        public ResiduoService(IResiduoRepository repository)
        {
            _repository = repository;
        }

        public void AtualizarResiduos(ResiduoModel residuo) => _repository.Update(residuo);

        public void CriarResiduos(ResiduoModel residuo) => _repository.Add(residuo);

        public void DeletarResiduos(int id)
        {
            var residuo = _repository.GetById(id);
            if (residuo != null)
            {
                _repository.Delete(residuo);
            }
        }

        public IEnumerable<ResiduoModel> ListarResiduos() => _repository.GetAll();

        public IEnumerable<ResiduoModel> ListarResiduos(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public ResiduoModel ObterResiduosPorId(int id) => _repository.GetById(id);
    }
}
