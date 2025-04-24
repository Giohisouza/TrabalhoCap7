using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public class RotaService : IRotaService
    {

        private readonly IRotaRepository _repository;

        public RotaService(IRotaRepository repository)
        {
            _repository = repository;
        }
        public void AtualizarRotas(RotaModel rota) => _repository.Update(rota);

        public void CriarRrotas(RotaModel rota) => _repository.Add(rota);

        public void DeletarRotas(int id)
        {
            var rota = _repository.GetById(id);
            if (rota != null)
            {
                _repository.Delete(rota);
            }
        }

        public IEnumerable<RotaModel> ListarRotas() => _repository.GetAll();

        public IEnumerable<RotaModel> ListarRotas(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }


        public RotaModel ObterRotasPorId(int id) => _repository.GetById(id);
    }
}
